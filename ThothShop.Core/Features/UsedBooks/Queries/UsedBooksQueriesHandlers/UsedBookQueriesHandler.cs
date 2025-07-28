using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.UsedBooks.Queries.Models;
using ThothShop.Core.Features.UsedBooks.Queries.Responses;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.UsedBooks.Queries.UsedBooksQueriesHandlers
{
    public class UsedBookQueriesHandler:ResponseHandler
                                         , IRequestHandler<GetAllUsedBooksQueryModel, Response<IEnumerable<GetUsedBookResponse>>>
                                         , IRequestHandler<GetUsedBookByIdQueryModel, Response<GetUsedBookResponse>>
                                         , IRequestHandler<SearchUsedBooksQueryModel, Response<IEnumerable<GetUsedBookResponse>>>
                                         , IRequestHandler<GetMyBookShelfQueryModel, Response<IEnumerable<GetUsedBookResponse>>>
                                         , IRequestHandler<FilterUsedBooksByConditionQueryModel, Response<IEnumerable<GetUsedBookResponse>>>
    {
        #region Field(s)
        private readonly IUsedBookService _usedBookService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public UsedBookQueriesHandler(IUsedBookService usedBookService, IMapper mapper)
        {
            _usedBookService = usedBookService;
            _mapper = mapper;
        }
        #endregion


        #region HandleMethod(s)
        public async Task<Response<IEnumerable<GetUsedBookResponse>>> Handle(GetAllUsedBooksQueryModel request, CancellationToken cancellationToken)
        {
            var usedBooks = await _usedBookService.GetAllUsedBooksAsync();
            var usedBookResponse = _mapper.Map<IEnumerable<GetUsedBookResponse>>(usedBooks);
            return Success(usedBookResponse);
        }
        public async Task<Response<GetUsedBookResponse>> Handle(GetUsedBookByIdQueryModel request, CancellationToken cancellationToken)
        {
            var usedBook = await _usedBookService.GetUsedBookByIdAsync(request.Id);
            var usedBookResponse = _mapper.Map<GetUsedBookResponse>(usedBook);
            return Success(usedBookResponse);
        }
        public async Task<Response<IEnumerable<GetUsedBookResponse>>> Handle(SearchUsedBooksQueryModel request, CancellationToken cancellationToken)
        {
            var usedBooks = await _usedBookService.SearchUsedBooks(request.SearchString);
            var usedBookResponse = _mapper.Map<IEnumerable<GetUsedBookResponse>>(usedBooks);
            return Success(usedBookResponse);
        }
        public async Task<Response<IEnumerable<GetUsedBookResponse>>> Handle(GetMyBookShelfQueryModel request, CancellationToken cancellationToken)
        {
            var usedBooks = await _usedBookService.GetUsedBooksByOwnerAsync(request.UserId);
            var usedBookResponse = _mapper.Map<IEnumerable<GetUsedBookResponse>>(usedBooks);
            return Success(usedBookResponse);
        }

        public async Task<Response<IEnumerable<GetUsedBookResponse>>> Handle(FilterUsedBooksByConditionQueryModel request, CancellationToken cancellationToken)
        {
            var usedBooks = await _usedBookService.GetUsedBooksByConditionAsync(request.UsedBookCondition);
            var usedBookResponse = _mapper.Map<IEnumerable<GetUsedBookResponse>>(usedBooks);
            return Success(usedBookResponse);
        }
        #endregion
    }
}
