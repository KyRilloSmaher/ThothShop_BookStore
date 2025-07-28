using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Reviews.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.Reviews.Queries.Models
{
    public class GetReviewByIdQueryModel : IRequest<Response<ReviewResponse>>
    {
        public Guid Id { get; private set; }

        public GetReviewByIdQueryModel(Guid id)
        {
            Id = id;
        }
    }
} 