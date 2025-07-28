using AutoMapper;
using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Authors.Queries.Models;
using ThothShop.Core.Features.Authors.Queries.Responses;
using ThothShop.Service.Wrappers;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Service.Contract;
using ThothShop.Service.implementations;
using ThothShop.Core.Features.Books.Queries.Responses;

namespace ThothShop.Core.Features.Authors.Queries.QueriesHandlers
{
    public class AuthorQueriesHandler : ResponseHandler
                                        ,IRequestHandler<GetAuthorByIdQueryModel,Response<GetAuthorResponse>>
                                        ,IRequestHandler<GetAllAuthorsQueryModel,Response<IEnumerable<GetAuthorResponse>>>
                                        ,IRequestHandler<GetAuthorsCountQueryModel,Response<int>>
                                        ,IRequestHandler<GetAllAuthorsByPaginationQueryModel, PaginatedResult<GetAuthorPaginatedListResponse>>
                                        ,IRequestHandler<GetAuthorsFilteredQueryModel, PaginatedResult<GetAuthorPaginatedListResponse>>
                                        ,IRequestHandler<GetPopularAuthorsQueryModel, PaginatedResult<GetAuthorPaginatedListResponse>>
    {
        #region Feild(s)
         private readonly IAuhtorService _auhtorService;
         private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public AuthorQueriesHandler(IAuhtorService auhtorService, IMapper mapper)
        {
            _auhtorService = auhtorService ?? throw new ArgumentNullException(nameof(auhtorService));
            _mapper = mapper ??  throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Method(s) Handle

        public async Task<Response<GetAuthorResponse>> Handle(GetAuthorByIdQueryModel request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            var author = await _auhtorService.GetAuthorByIdAsync(request.Id);
            if (author == null) { 
              return NotFound<GetAuthorResponse>();
            }
            var mappedAuthor = _mapper.Map<GetAuthorResponse>(author);
            return Success(mappedAuthor);
        }

        

        public async Task<Response<IEnumerable<GetAuthorResponse>>> Handle(GetAllAuthorsQueryModel request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            var author = await _auhtorService.GetAllAuthorsAsync();
            if (author == null)
            {
                return NotFound<IEnumerable<GetAuthorResponse>>();
            }
            var mappedAuthor = _mapper.Map<IEnumerable<GetAuthorResponse>>(author);
            return Success(mappedAuthor);
        }

        public async Task<PaginatedResult<GetAuthorPaginatedListResponse>> Handle(GetAllAuthorsByPaginationQueryModel request, CancellationToken cancellationToken)
        {
            var AuthorList = await _auhtorService.GetAllAuthorsByPaginationAsync(request.PageNumber, request.PageSize);
            IEnumerable<GetAuthorPaginatedListResponse> mappedAuthor = _mapper.Map<IEnumerable<GetAuthorPaginatedListResponse>>(AuthorList);
            PaginatedResult<GetAuthorPaginatedListResponse> PaginatedList = new PaginatedResult<GetAuthorPaginatedListResponse>(mappedAuthor);
            PaginatedList.PageSize = mappedAuthor.Count();
            PaginatedList.CurrentPage = request.PageNumber == 0 ? 1 : request.PageNumber;
            var total =  _auhtorService.GetAllAuthorsAsync().Result.Count();
            PaginatedList.TotalCount = total;
            PaginatedList.TotalPages = (int)Math.Ceiling((double)total/ request.PageSize);
            PaginatedList.Succeeded = true;
            return PaginatedList;
        }

        public async Task<PaginatedResult<GetAuthorPaginatedListResponse>> Handle(GetAuthorsFilteredQueryModel request, CancellationToken cancellationToken)
        {

            var query =  _auhtorService.FilterAuthors(request);
            var paginatedAuthorList = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            IEnumerable<GetAuthorPaginatedListResponse> mappedAuthor = _mapper.Map<IEnumerable<GetAuthorPaginatedListResponse>>(paginatedAuthorList.Data);

            return PaginatedResult<GetAuthorPaginatedListResponse>.Success(mappedAuthor, paginatedAuthorList.TotalCount, paginatedAuthorList.CurrentPage, paginatedAuthorList.PageSize);
        }

        public async Task<PaginatedResult<GetAuthorPaginatedListResponse>> Handle(GetPopularAuthorsQueryModel request, CancellationToken cancellationToken)
        {
            int total;
            IEnumerable<Author> AuthorList;
            (AuthorList, total) = await _auhtorService.GetPopularAuthorsAsync(request.PageNumber, request.PageSize);
            IEnumerable<GetAuthorPaginatedListResponse> mappedAuthor = _mapper.Map<IEnumerable<GetAuthorPaginatedListResponse>>(AuthorList);
            PaginatedResult<GetAuthorPaginatedListResponse> PaginatedList = new PaginatedResult<GetAuthorPaginatedListResponse>(mappedAuthor);
            PaginatedList.PageSize = mappedAuthor.Count();
            PaginatedList.CurrentPage = request.PageNumber == 0 ? 1 : request.PageNumber;
            PaginatedList.TotalCount = total;
            PaginatedList.TotalPages = (int)Math.Ceiling((double)total / request.PageSize);
            PaginatedList.Succeeded = true;
            return PaginatedList;
        }

        public async Task<Response<int>> Handle(GetAuthorsCountQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _auhtorService.GetAuthorsCountAsync();
            return Success<int>(result);
        }

        #endregion
    }
}
