using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Books.Commands.Models;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Books.Commands.CommandsHandlers
{
    public class BookCommandsHandler : ResponseHandler
                                      , IRequestHandler<CreateBookCommandModel, Response<string>>
                                      , IRequestHandler<UpdateBookCommandModel, Response<string>>
                                      , IRequestHandler<DeleteBookCommandModel, Response<string>>
                                      , IRequestHandler<IncreaseBookStockCommandModel, Response<string>>
                                      , IRequestHandler<DecreaseBookStockCommandModel , Response<string>>
    {
        #region Field(s)
        private readonly IBookService _bookService;
        private readonly IImageUploaderService _imageUploaderService;
        private readonly IBookImageService _bookImageService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor(s)

        public BookCommandsHandler(IBookService bookService, IImageUploaderService imageUploaderService, IBookImageService bookImageService, IMapper mapper)
        {
            _bookService = bookService;
            _imageUploaderService = imageUploaderService;
            _bookImageService = bookImageService;
            _mapper = mapper;
        }

        #endregion

        #region HandleMethod(s)


        public async Task<Response<string>> Handle(CreateBookCommandModel request, CancellationToken cancellationToken)
        {
            var primaryImage = request.PrimaryImage;
            var images = request.Images;
            var mapppedBook = _mapper.Map<Book>(request);
            var result = await _bookService.CreateBookAsync(mapppedBook,primaryImage,images);
            if (result.Equals(SystemMessages.FailedToAddEntity))
            {
                return Failed<String>("Book not created");
            }
            return Success<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateBookCommandModel request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBookByIdAsync(request.Id);
            if (book is null)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToUpdateEntity, "Book"));
            }
            var primaryImage = request.PrimaryImage;
            var images = request.Images;
             _mapper.Map(request,book);
            var result = await _bookService.UpdateBookAsync(book, primaryImage, images);
            if (!result)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToUpdateEntity, "Book"));
            }
            return Success<string>(string.Format(SystemMessages.UpdatedSuccessfully, "Book"));
        }

        public async Task<Response<string>> Handle(DeleteBookCommandModel request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBookByIdAsync(request.Id);
            if (book is null)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToDeleteEntity, "Book"));
            }

            var result = await _bookService.DeleteBookAsync(book);
            if (!result)
            {
                return Failed<string>(string.Format(SystemMessages.FailedToDeleteEntity,"Book"));
            }
            return Success<string>(string.Format(SystemMessages.DeletedSuccessfully,"Book"));
        }

        public async Task<Response<string>> Handle(IncreaseBookStockCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _bookService.IncreaseBookStock(request.BookId, request.amount);
            if (result)
            {
                Success<string>(string.Format(SystemMessages.UpdatedSuccessfully, "Book"));
            }
            return Failed<string>(string.Format(SystemMessages.FailedToUpdateEntity, "Book"));
        }

        public async Task<Response<string>> Handle(DecreaseBookStockCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _bookService.DecreaseBookStock(request.BookId, request.amount);
            if (result)
            {
                Success<string>(string.Format(SystemMessages.UpdatedSuccessfully, "Book"));
            }
            return Failed<string>(string.Format(SystemMessages.FailedToUpdateEntity, "Book"));
        }

        #endregion
    }
}
