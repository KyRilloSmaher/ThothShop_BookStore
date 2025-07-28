using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Features.Carts.Queries.Responses
{
    public class Cartresponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemResponse> CartItems { get; set; }

    }
}
