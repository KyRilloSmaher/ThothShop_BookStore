using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Authors.Queries.Responses;
using ThothShop.Core.Features.Books.Queries.Models;
using ThothShop.Core.Features.Books.Queries.Responses;

using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Service.Commans;
using ThothShop.Service.Contract;
using ThothShop.Service.implementations;
using ThothShop.Service.Wrappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ThothShop.Core.Features.Books.Queries.QueriesHandlers
{
    public class BooksQueryHandler : ResponseHandler,
        IRequestHandler<GetAllAuthorsAssociatedWithABookQueryModel, Response<IEnumerable<GetAuthorResponse>>>,
        IRequestHandler<GetAllOutOfStockBooksQueryModel, Response<IEnumerable<GetBookResponseForDashboard>>>,
        IRequestHandler<GetAllBooksBySpecificAuthorQueryModel, PaginatedResult<GetBookResponse>>,
        IRequestHandler<GetAllBooksPagintedQueryModel, PaginatedResult<GetBookResponse>>,
        IRequestHandler<GetAllBooksInASpecificCategoryQueryModel, PaginatedResult<GetBookResponse>>,
        IRequestHandler<GetBookByIdQueryModel, Response<GetBookResponse>>,
        IRequestHandler<GetBooksByBothAuthorAndCategoryQueyModel, Response<IEnumerable<GetBookResponse>>>,
        IRequestHandler<GetBooksPagintedOrderedByStockAscendingQueryModel, PaginatedResult<GetBookResponseForDashboard>>,
        IRequestHandler<GetBooksPagintedOrderedByStockDescendingQueryModel, PaginatedResult<GetBookResponseForDashboard>>,
        IRequestHandler<GetBooksWithStockBelowAThresholdQueryModel, Response<IEnumerable<GetBookResponse>>>,
        IRequestHandler<GetNewlyReleasedBooksQueryModel, Response<IEnumerable<GetBookResponse>>>,
        IRequestHandler<GetsBooksByAnAuthorOorderedByViewCountQueryModel, Response<IEnumerable<GetBookResponse>>>,
        IRequestHandler<GetSimilarBooksQueryModel, Response<IEnumerable<GetBookResponse>>>,
        IRequestHandler<GetTheTotalNumberOfBooksQueryModel, Response<int>>,
        IRequestHandler<GetTopRatedBooksPagintedQueryModel, PaginatedResult<GetBookResponse>>,
        IRequestHandler<GetTopSellingBooksQueryModel, Response<IEnumerable<GetBookResponseForDashboard>>>,
        IRequestHandler<SearchBookQueryModel, Response<IEnumerable<GetBookResponse>>>
    {
        private readonly IBookService _bookService;
        private readonly IAuhtorService _authorService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public BooksQueryHandler(
            IBookService bookService,
            IAuhtorService authorService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Single Result Handlers

        public async Task<Response<GetBookResponse>> Handle(GetBookByIdQueryModel request, CancellationToken cancellationToken)
        {
            var existingBook = await _bookService.GetBookByIdIncludingCategoryAsNotrackingAsync(request.Id);
            if (existingBook == null)
            {
                return NotFound<GetBookResponse>(string.Format(SystemMessages.NotFound, "Book"));
            }
            return Success(_mapper.Map<GetBookResponse>(existingBook));
        }

        #endregion

        #region Collection Handlers (Non-Paginated)

        public async Task<Response<IEnumerable<GetBookResponse>>> Handle(SearchBookQueryModel request, CancellationToken cancellationToken)
        {
            var books = await _bookService.SearchBooksQueryable(request as FilterListParams);
            return HandleBookCollectionResponse(books);
        }

        public async Task<Response<IEnumerable<GetBookResponseForDashboard>>> Handle(GetTopSellingBooksQueryModel request, CancellationToken cancellationToken)
        {
            var books = await Task.FromResult(_bookService.GetTopSellingBooks());
            return HandleBookCollectionDashboardResponse(books);
        }

        public async Task<Response<int>> Handle(GetTheTotalNumberOfBooksQueryModel request, CancellationToken cancellationToken)
        {
            var totalBooks = await _bookService.GetTotalBooksCountAsync();
            return Success(totalBooks);
        }

        public async Task<Response<IEnumerable<GetAuthorResponse>>> Handle(GetAllAuthorsAssociatedWithABookQueryModel request, CancellationToken cancellationToken)
        {
            var isBookExist = await _bookService.IsBookIdExits(request.BookId);
            if (!isBookExist)
            {
                return NotFound<IEnumerable<GetAuthorResponse>>(string.Format(SystemMessages.NotFound, "Book"));
            }

            var authors = await _bookService.GetAuthorsForBookAsync(request.BookId);
            return HandleAuthorCollectionResponse(authors);
        }

        public async Task<Response<IEnumerable<GetBookResponseForDashboard>>> Handle(GetAllOutOfStockBooksQueryModel request, CancellationToken cancellationToken)
        {
            var books = await _bookService.GetOutOfStockBooksAsync();
            return HandleBookCollectionDashboardResponse(books);
        }

        public async Task<Response<IEnumerable<GetBookResponse>>> Handle(GetBooksWithStockBelowAThresholdQueryModel request, CancellationToken cancellationToken)
        {
            var books =  await Task.FromResult( _bookService.GetLowStockBooks(request.Threshold));
            return HandleBookCollectionResponse(books);
        }

        public async Task<Response<IEnumerable<GetBookResponse>>> Handle(GetNewlyReleasedBooksQueryModel request, CancellationToken cancellationToken)
        {
            var books =  await Task.FromResult(_bookService.GetNewReleases());
            return HandleBookCollectionResponse(books);
        }

        public async Task<Response<IEnumerable<GetBookResponse>>> Handle(GetsBooksByAnAuthorOorderedByViewCountQueryModel request, CancellationToken cancellationToken)
        {
            var books = await _bookService.GetBooksByAuthorOrderedByViewsAsync(request.AuthorId);
            return HandleBookCollectionResponse(books);
        }

        public async Task<Response<IEnumerable<GetBookResponse>>> Handle(GetSimilarBooksQueryModel request, CancellationToken cancellationToken)
        {
            var books =  await Task.FromResult(_bookService.GetSimilarBooks(request.BookId));
            return HandleBookCollectionResponse(books);
        }

        public async Task<Response<IEnumerable<GetBookResponse>>> Handle(GetBooksByBothAuthorAndCategoryQueyModel request, CancellationToken cancellationToken)
        {
            var books = await _bookService.GetBooksByAuthorAndCategoryAsync(request.AuthorId, request.CategoryId);
            return HandleBookCollectionResponse(books);
        }

        #endregion

        #region Paginated Handlers

        public async Task<PaginatedResult<GetBookResponse>> Handle(GetAllBooksPagintedQueryModel request, CancellationToken cancellationToken)
        {
            var query = await _bookService.GetAllBooksAsync();
            var paginatedBooks = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            var mappedBooks = _mapper.Map<IEnumerable<GetBookResponse>>(paginatedBooks.Data);

            return PaginatedResult<GetBookResponse>.Success(mappedBooks, paginatedBooks.TotalCount, paginatedBooks.CurrentPage, paginatedBooks.PageSize);

        }

        public async Task<PaginatedResult<GetBookResponse>> Handle(GetAllBooksBySpecificAuthorQueryModel request, CancellationToken cancellationToken)
        {
            var query = await _bookService.GetBooksByAuthorAsync(request.AuthorId); 

            var paginatedBooks = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            var mappedBooks = _mapper.Map<IEnumerable<GetBookResponse>>(paginatedBooks.Data);

            return PaginatedResult<GetBookResponse>.Success(mappedBooks, paginatedBooks.TotalCount, paginatedBooks.CurrentPage, paginatedBooks.PageSize);

        }

        public async Task<PaginatedResult<GetBookResponse>> Handle(GetAllBooksInASpecificCategoryQueryModel request, CancellationToken cancellationToken)
        {
             var query = await _bookService.GetBooksByCategoryAsync(request.CategoryId);
            var books = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            var mappedBooks = _mapper.Map<IEnumerable<GetBookResponse>>(books.Data);
            return PaginatedResult<GetBookResponse>.Success(mappedBooks,books.TotalCount, books.CurrentPage, books.PageSize);
        }

        public async Task<PaginatedResult<GetBookResponseForDashboard>> Handle(GetBooksPagintedOrderedByStockAscendingQueryModel request, CancellationToken cancellationToken)
        {
            var query = await _bookService.GetBooksOrderedByStockAscendingAsync();
            var books = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            var mappedBooks = _mapper.Map<IEnumerable<GetBookResponseForDashboard>>(books.Data);
            return PaginatedResult<GetBookResponseForDashboard>.Success(mappedBooks, books.TotalCount, books.CurrentPage, books.PageSize);
        }       

        public async Task<PaginatedResult<GetBookResponseForDashboard>> Handle(GetBooksPagintedOrderedByStockDescendingQueryModel request, CancellationToken cancellationToken)
        {
            var query = await _bookService.GetBooksOrderedByStockDescendingAsync();
            var books = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            var mappedBooks = _mapper.Map<IEnumerable<GetBookResponseForDashboard>>(books.Data);
            return PaginatedResult<GetBookResponseForDashboard>.Success(mappedBooks, books.TotalCount, books.CurrentPage, books.PageSize);
        }

        public async Task<PaginatedResult<GetBookResponse>> Handle(GetTopRatedBooksPagintedQueryModel request, CancellationToken cancellationToken)
        {
            var query =  _bookService.GetTopRatedBooks();
            var books = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            var mappedBooks = _mapper.Map<IEnumerable<GetBookResponse>>(books.Data);
            return PaginatedResult<GetBookResponse>.Success(mappedBooks, books.TotalCount, books.CurrentPage, books.PageSize);
        }

        #endregion

        #region Private Helper Methods

        private Response<IEnumerable<GetBookResponse>> HandleBookCollectionResponse(IEnumerable<Book> books)
        {
            if (books == null || !books.Any())
            {
                return Failed<IEnumerable<GetBookResponse>>(string.Format(SystemMessages.NotFound, "Books"));
            }
            return Success(_mapper.Map<IEnumerable<GetBookResponse>>(books));
        }
        private Response<IEnumerable<GetBookResponseForDashboard>> HandleBookCollectionDashboardResponse(IEnumerable<Book> books)
        {
            if (books == null || !books.Any())
            {
                return Failed<IEnumerable<GetBookResponseForDashboard>>(string.Format(SystemMessages.NotFound, "Books"));
            }
            return Success(_mapper.Map<IEnumerable<GetBookResponseForDashboard>>(books));
        }
        private Response<IEnumerable<GetAuthorResponse>> HandleAuthorCollectionResponse(IEnumerable<Author> authors)
        {
            if (authors == null || !authors.Any())
            {
                return Failed<IEnumerable<GetAuthorResponse>>(string.Format(SystemMessages.NotFound, "Authors"));
            }
            return Success(_mapper.Map<IEnumerable<GetAuthorResponse>>(authors));
        }


        #endregion
    }
}