using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Authors.Queries.Responses;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class GetAllAuthorsAssociatedWithABookQueryModel : IRequest<Response<IEnumerable<GetAuthorResponse>>>
    {
        public GetAllAuthorsAssociatedWithABookQueryModel(Guid bookId)
        {
            BookId = bookId;
        }

        public Guid BookId { get; set; }
    }
}
