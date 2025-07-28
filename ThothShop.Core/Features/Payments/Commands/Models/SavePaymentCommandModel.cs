using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Domain.Enums;

namespace ThothShop.Core.Features.Payments.Commands.Models
{
    public class SavePaymentCommandModel:IRequest<Response<string>>
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string? TransactionId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Stripe;
        public PaymentStatus Status { get; set; } = PaymentStatus.Completed;
    }
}
