using ThothShop.Core.Bases;
using ThothShop.Core.Features.Books.Queries.Responses;
using MediatR;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class GetSimilarBooksQueryModel : IRequest<Response<IEnumerable<GetBookResponse>>>
    {
        public GetSimilarBooksQueryModel(Guid bookId)
        {
            BookId = bookId;
        }
        public int Count { get; set; } = 10; // Default value for the number of similar books to retrieve
        public Guid BookId { get; set; }
    }
}
