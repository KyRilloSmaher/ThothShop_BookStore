using MediatR;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Reviews.Commands.Models
{
    public class DeleteReviewCommandModel : IRequest<Response<string>>
    {
        public Guid Id { get; private set; }

        public DeleteReviewCommandModel(Guid id)
        {
            Id = id;
        }
    }
} 