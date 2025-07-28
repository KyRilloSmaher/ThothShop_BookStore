using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Authors.Queries.Models.Base;
using ThothShop.Core.Features.Authors.Queries.Responses;
using ThothShop.Service.Wrappers;

namespace ThothShop.Core.Features.Authors.Queries.Models
{
    public class GetPopularAuthorsQueryModel :PagintedRequest, IRequest<PaginatedResult<GetAuthorPaginatedListResponse>>
    {
    }
}
