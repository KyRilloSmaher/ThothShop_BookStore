using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Reviews.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.Reviews.Queries.Models
{
    public class GetReviewsByUserQueryModel : IRequest<Response<IEnumerable<ReviewResponse>>>
    {
        public string UserId { get; private set; }

        public GetReviewsByUserQueryModel(string userId)
        {
            UserId = userId;
        }
    }
} 