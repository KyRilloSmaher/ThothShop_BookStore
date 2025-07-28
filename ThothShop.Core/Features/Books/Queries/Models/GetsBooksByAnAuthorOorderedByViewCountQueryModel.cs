using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Books.Queries.Responses;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class GetsBooksByAnAuthorOorderedByViewCountQueryModel : IRequest<Response<IEnumerable<GetBookResponse>>>
    {
        public GetsBooksByAnAuthorOorderedByViewCountQueryModel(Guid authorId)
        {
            AuthorId = authorId;
        }

        public Guid AuthorId { get; set; }
    }
}
