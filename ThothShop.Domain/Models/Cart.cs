using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
    public class Cart 
    {
     
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public User User { get; set; }
        public decimal TotalPrice { get; set; }

        // Constructor(s)
        public Cart() { }
        public Cart(Guid id, string userId, List<CartItem> items)
        {
            Id = id;
            UserId = userId;
            Items = items;
            TotalPrice = CalculateTotal();
        }

        public decimal CalculateTotal() => Items.Sum(i => i.Quantity * i.UnitPrice);
    }
}
