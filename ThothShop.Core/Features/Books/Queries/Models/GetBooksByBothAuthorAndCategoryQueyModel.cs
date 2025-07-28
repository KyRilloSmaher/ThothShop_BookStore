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
    public class GetBooksByBothAuthorAndCategoryQueyModel : IRequest<Response<IEnumerable<GetBookResponse>>>
    {
        public GetBooksByBothAuthorAndCategoryQueyModel(Guid authorId, Guid categoryId)
        {
            AuthorId = authorId;
            CategoryId = categoryId;
        }

        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }

        
    }
}
