
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Carts.Queries.Responses;

namespace ThothShop.Core.Features.Cart.Queries.Models
{
    public class GetCartItemsQueryModel : IRequest<Response<IEnumerable<CartItemResponse>>>
    {
        public GetCartItemsQueryModel(Guid cartId)
        {
            CartId = cartId;
        }

        public Guid CartId { get; set; }
    }
}
