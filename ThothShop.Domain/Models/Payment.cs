using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;

namespace ThothShop.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CreditCard;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string? TransactionId { get;  set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Order? Order { get; set; }
    }
}
