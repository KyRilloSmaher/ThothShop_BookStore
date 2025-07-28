using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Users.Commands.Models;
using ThothShop.Domain.Helpers;
using ThothShop.Service;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Users.Commands.Handlers
{
    public class UserCommandsHandlers :ResponseHandler
                                         , IRequestHandler<CreateUserCommandModel, Response<string>>
                                         , IRequestHandler<CreateAdminCommandModel, Response<string>>
                                         , IRequestHandler<DeleteUserCommandModel, Response<string>>
                                         , IRequestHandler<UpdateUserCommandModel, Response<string>>
    {
        #region Field(s)
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public UserCommandsHandlers(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        #endregion

        #region HandleMethod(s)
        public async Task<Response<string>> Handle(CreateUserCommandModel request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Models.User>(request);
            var result = await _userService.AddUser(user,request.Password,"User");
            switch (result)
            {
                case SystemMessages.UserNameAlreadyExists:
                    return BadRequest<string>(result);
                case SystemMessages.EmailAlreadyExists:
                    return BadRequest<string>(result);
                case SystemMessages.FailedToAddEntity:
                    return Failed<string>(result);
                default: return Success(SystemMessages.AddedSuccessfully);

            }
        }

        public async Task<Response<string>> Handle(CreateAdminCommandModel request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Models.User>(request);
            var result = await _userService.AddUser(user,request.Password, "Admin");
            switch (result)
            {
                case SystemMessages.UserNameAlreadyExists:
                    return BadRequest<string>(result);
                case SystemMessages.EmailAlreadyExists:
                    return BadRequest<string>(result);
                case SystemMessages.FailedToAddEntity:
                    return Failed<string>(result);
                default: return Success(SystemMessages.AddedSuccessfully);

            }
        }

        public async  Task<Response<string>> Handle(DeleteUserCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _userService.DeleteUserAsync(request.Id);
            return result ? Deleted<string>(string.Format(SystemMessages.DeletedSuccessfully,"User")) : Failed<string>(string.Format(SystemMessages.FailedToDeleteEntity,"User"));
        }

        public async Task<Response<string>> Handle(UpdateUserCommandModel request, CancellationToken cancellationToken)
        {
            var ExistingUser = await _userService.GetUserByIdAsync(request.Id,true);
            if (ExistingUser == null)
                return NotFound<string>(string.Format(SystemMessages.EntityNotFound, "User"));
            _mapper.Map(request, ExistingUser);
            var result = await _userService.UpdateUserAsync(ExistingUser);

            return result ? Success<string>(string.Format(SystemMessages.UpdatedSuccessfully, "User")) : Failed<string>(string.Format(SystemMessages.FailedToUpdateEntity, "User"));

        }
        #endregion
    }

}
