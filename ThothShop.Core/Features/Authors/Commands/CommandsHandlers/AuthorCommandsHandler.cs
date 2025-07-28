using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Authors.Commands.Models;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;
using ThothShop.Service.implementations;

namespace ThothShop.Core.Features.Authors.Commands.CommandsHandlers
{
    public class AuthorCommandsHandler:ResponseHandler
                                        , IRequestHandler<CreateAuthorCommandModel, Response<string>>
                                        , IRequestHandler<UpdateAuthorCommandModel, Response<string>>
                                        , IRequestHandler<DeleteAuthorCommandModel, Response<string>>
    {
        #region Feild(s)
        private readonly IAuhtorService _authorService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public AuthorCommandsHandler(IAuhtorService auhtorService, IMapper mapper)
        {
            _authorService = auhtorService ?? throw new ArgumentNullException(nameof(auhtorService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion
        #region Method(s) Handle
        public async Task<Response<string>> Handle(CreateAuthorCommandModel request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            var author = _mapper.Map<Author>(request);
            var result = await _authorService.CreateAuthorAsync(author ,request.PrimaryImage,request.Images);
            if (result.Equals(string.Format(SystemMessages.CreatedSuccessfully, "Auhtor")))
            {
                return Success(SystemMessages.Success);
            }
            return Failed<string>(SystemMessages.FailedToAddEntity);
        }

        public async Task<Response<string>> Handle(UpdateAuthorCommandModel request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var oldAuthor = await _authorService.GetAuthorByIdAsync(request.Id,true);
            if (oldAuthor is null)
                return Failed<string>(SystemMessages.EntityNotFound);

            // Apply changes from request to tracked entity
            _mapper.Map(request,oldAuthor);

            var result = await _authorService.UpdateAuthorAsync(oldAuthor,request.primaryImage , request.Images);

            if (result)
            {
                return Success(SystemMessages.Success);
            }

            return Failed<string>(SystemMessages.FailedToAddEntity);
        }

        public async Task<Response<string>> Handle(DeleteAuthorCommandModel request, CancellationToken cancellationToken)
        {
            var ExistingAuthor = await _authorService.GetAuthorByIdAsync(request.Id, true);
            if (ExistingAuthor is null)
                return Failed<string>(SystemMessages.EntityNotFound);
            var result = await _authorService.DeleteAuthorAsync(ExistingAuthor);
            if (result)
            {
                return Deleted<string>(string.Format(SystemMessages.DeletedSuccessfully,"Author"));
            }
            return Failed<string>(SystemMessages.FailedToDeleteEntity);
        }

        #endregion
    }
}
