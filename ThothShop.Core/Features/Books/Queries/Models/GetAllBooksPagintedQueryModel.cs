using MediatR;
using ThothShop.Core.Features.Authors.Queries.Models.Base;
using ThothShop.Core.Features.Authors.Queries.Responses;
using ThothShop.Core.Features.Books.Queries.Responses;
using ThothShop.Service.Wrappers;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class GetAllBooksPagintedQueryModel : PagintedRequest ,IRequest<PaginatedResult<GetBookResponse>>
    {
    }
}
