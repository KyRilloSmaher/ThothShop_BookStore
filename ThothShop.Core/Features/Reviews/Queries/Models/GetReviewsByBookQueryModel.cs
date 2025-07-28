using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Reviews.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.Reviews.Queries.Models
{
    public class GetReviewsByBookQueryModel : IRequest<Response<IEnumerable<ReviewResponse>>>
    {
        public Guid BookId { get; private set; }

        public GetReviewsByBookQueryModel(Guid bookId)
        {
            BookId = bookId;
        }
    }
} 