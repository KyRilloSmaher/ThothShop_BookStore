using MediatR;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Carts.Commands.Models
{
    public class RemoveFromCartCommandModel : IRequest<Response<string>>
    {
        public Guid CartId { get; private set; }
        public Guid BookId { get; private set; }
    }
} 