using MediatR;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Carts.Queries.Models
{
    public class GetCartTotalQueryModel : IRequest<Response<decimal>>
    {
        public GetCartTotalQueryModel(Guid cartId)
        {
            CartId = cartId;
        }

        public Guid CartId { get; set; }
    }
} 