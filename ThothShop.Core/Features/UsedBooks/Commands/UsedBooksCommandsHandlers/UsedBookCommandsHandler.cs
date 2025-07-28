using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.UsedBooks.Commands.Models;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;
using ThothShop.Service.implementations;

namespace ThothShop.Core.Features.UsedBooks.Commands.UsedBooksCommandsHandlers
{
    public class UsedBookCommandsHandler: ResponseHandler
                                , IRequestHandler<CreateUsedBookCommandModel, Response<string>>
                                , IRequestHandler<DeleteUsedBookCommandModel, Response<string>>
                                , IRequestHandler<UpdateUsedBookCommandModel, Response<string>>
    {
        #region Field(s)
        private readonly IUsedBookService _usedBookService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public UsedBookCommandsHandler(IUsedBookService usedBookService, IMapper mapper)
        {
            _usedBookService = usedBookService;
            _mapper = mapper;
        }
        #endregion


        #region HandleMethod(s)
        public async Task<Response<string>> Handle(CreateUsedBookCommandModel request, CancellationToken cancellationToken)
        {
            var primaryImage = request.PrimaryImage;
            var images = request.Images;
            var usedBook = _mapper.Map<UsedBook>(request);
            var result = await _usedBookService.CreateUsedBookAsync(usedBook, primaryImage, images);
            return Success(result);
        }

        public async Task<Response<string>> Handle(UpdateUsedBookCommandModel request, CancellationToken cancellationToken)
        {
            var book = await _usedBookService.GetUsedBookByIdAsync(request.Id);
            if (book is null)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToUpdateEntity, "Book"));
            }
            var primaryImage = request.PrimaryImage;
            var images = request.Images;
            _mapper.Map(request, book);
            var result = await _usedBookService.UpdateUsedBookAsync(book, primaryImage, images);
            if (!result)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToUpdateEntity, "Book"));
            }
            return Success<string>(string.Format(SystemMessages.UpdatedSuccessfully, "Book"));
        }

        public async  Task<Response<string>> Handle(DeleteUsedBookCommandModel request, CancellationToken cancellationToken)
        {
          
            var usedBook = await _usedBookService.GetUsedBookByIdAsync(request.Id);
            if (usedBook is null)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToDeleteEntity, "Book"));
            }

            var result = await _usedBookService.DeleteUsedBookAsync(usedBook);
            if (!result)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToDeleteEntity, "Book"));
            }
            return Success<string>(string.Format(SystemMessages.DeletedSuccessfully, "Book"));
        }
        #endregion

    }
}
