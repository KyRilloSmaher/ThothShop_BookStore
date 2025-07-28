using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Users.Queries.Models;
using ThothShop.Core.Features.Users.Queries.Responses;
using ThothShop.Domain.Helpers;
using ThothShop.Infrastructure.Contracts;

namespace ThothShop.Core.Features.Users.Queries.Handlers
{
    public class UserQueriesHandler : ResponseHandler ,
        IRequestHandler<GetUserByUserNameQueryModel, Response<UserResponse>>,
        IRequestHandler<GetAllUsersQueryModel, Response<IEnumerable<UserResponse>>>,
        IRequestHandler<GetAllAdminsQueryModel, Response<IEnumerable<UserResponse>>>,
        IRequestHandler<GetUserByIdQueryModel, Response<UserResponse>>

    {
        #region Field(s)
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public UserQueriesHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        #region HandleMethod(s)
        public async Task<Response<UserResponse>> Handle(GetUserByUserNameQueryModel request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.UserName);
            if (user == null) {
                return NotFound<UserResponse>(string.Format(SystemMessages.NotFound, "User"));
            }
            var userResponse = _mapper.Map<UserResponse>(user);
            return Success(userResponse);
        }
        public async Task<Response<IEnumerable<UserResponse>>> Handle(GetAllUsersQueryModel request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (users == null )
                return NotFound<IEnumerable<UserResponse>>(string.Format(SystemMessages.NotFound, "User"));
            var userResponses = _mapper.Map<IEnumerable<UserResponse>>(users);
            return Success(userResponses);
        }

        public async Task<Response<UserResponse>> Handle(GetUserByIdQueryModel request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return NotFound<UserResponse>(string.Format(SystemMessages.NotFound,"User"));
            var userResponse = _mapper.Map<UserResponse>(user);
            return Success(userResponse);
        }

        public async Task<Response<IEnumerable<UserResponse>>> Handle(GetAllAdminsQueryModel request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAdminsAsync();
            if (users == null)
                return NotFound<IEnumerable<UserResponse>>(string.Format(SystemMessages.NotFound, "User"));
            var userResponses = _mapper.Map<IEnumerable<UserResponse>>(users);
            return Success(userResponses);
        }
        #endregion
    }
}
