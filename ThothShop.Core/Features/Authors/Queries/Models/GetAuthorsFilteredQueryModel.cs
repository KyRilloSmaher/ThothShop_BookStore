using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Authors.Queries.Responses;
using ThothShop.Infrastructure.Bases;
using ThothShop.Service.Wrappers;

namespace ThothShop.Core.Features.Authors.Queries.Models
{
    public class GetAuthorsFilteredQueryModel:FilterAuthorModel,IRequest<PaginatedResult<GetAuthorPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
