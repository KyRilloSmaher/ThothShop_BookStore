using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Authors.Queries.Models.Base;
using ThothShop.Core.Features.Books.Queries.Responses;
using ThothShop.Service.Wrappers;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class GetTopRatedBooksPagintedQueryModel: PagintedRequest,IRequest<PaginatedResult<GetBookResponse>>
    {
        public int Count { get; set; } = 10;

    }
}
