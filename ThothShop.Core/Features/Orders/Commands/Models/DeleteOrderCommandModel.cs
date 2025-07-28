using MediatR;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Orders.Commands.Models
{
    public class DeleteOrderCommandModel : IRequest<Response<bool>>
    {
        public DeleteOrderCommandModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
} 