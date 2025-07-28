using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;

namespace ThothShop.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public User User { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        public int? PaymentId { get; set; } // Nullable to allow for orders without payments initially
        public Payment Payment { get; set; } // Navigation property for the payment

        // Constructor
        public Order()
        {
            Items = new HashSet<OrderItem>();
        }

        public Order(Guid id, string userId, List<OrderItem> items)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("Order must contain at least one book.", nameof(items));

            Id = id;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            Items = items;
            Status = OrderStatus.Pending;
            CalculateTotalAmount();
        }

        public void CalculateTotalAmount()
        {
            TotalPrice = Items.Sum(item => item.UnitPrice * item.Quantity);
        }
    }
}
