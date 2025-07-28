using MediatR;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Reviews.Queries.Models
{
    public class GetBookAverageRatingQueryModel : IRequest<Response<double>>
    {
        public Guid BookId { get; private set; }

        public GetBookAverageRatingQueryModel(Guid bookId)
        {
            BookId = bookId;
        }
    }
} 