using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }

        //constructors
        public OrderItem() { }
        public OrderItem(Guid bookId, Guid orderId, int quantity, decimal unitPrice)
        {
            BookId = bookId;
            OrderId = orderId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
