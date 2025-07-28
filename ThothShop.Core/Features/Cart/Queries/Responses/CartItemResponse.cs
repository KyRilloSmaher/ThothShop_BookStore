using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Features.Carts.Queries.Responses
{
    public class CartItemResponse
    {
        public CartItemResponse() { }
        public CartItemResponse(Guid id, Guid productId, string productName, decimal price, int quantity)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
