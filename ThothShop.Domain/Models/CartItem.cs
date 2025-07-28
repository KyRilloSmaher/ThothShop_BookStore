using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
   
    public class CartItem
    {
        public Guid CartId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Book Book { get; set; }
        [JsonIgnore]
        public Cart Cart { get; set; }
        public CartItem() { }
        public CartItem(Guid cartId, Guid bookId, int quantity, decimal unitPrice)
        {
            CartId = cartId;
            BookId = bookId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

    }
}
