using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Authors.Queries.Models.Base;
using ThothShop.Core.Features.Authors.Queries.Responses;
using ThothShop.Core.Features.Books.Queries.Responses;
using ThothShop.Service.Wrappers;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class GetAllBooksBySpecificAuthorQueryModel : PagintedRequest ,IRequest<PaginatedResult<GetBookResponse>>
    {
        public GetAllBooksBySpecificAuthorQueryModel(Guid authorId)
        {
            AuthorId = authorId;
        }

        public Guid AuthorId { get; set; }
    }
}
