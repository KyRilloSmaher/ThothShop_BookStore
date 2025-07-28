using MediatR;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Orders.Commands.Models
{
    public class CreateOrderFromCartCommandModel : IRequest<Response<Guid>>
    {
        public CreateOrderFromCartCommandModel(Guid cartId)
        {
            CartId = cartId;
        }

        public Guid CartId { get; set; }
    }
} 