using MediatR;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Carts.Queries.Models
{
    public class GetCartItemCountQueryModel : IRequest<Response<int>>
    {
        public GetCartItemCountQueryModel(Guid cartId)
        {
            CartId = cartId;
        }

        public Guid CartId { get; set; }
    }
} 