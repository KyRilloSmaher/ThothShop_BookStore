using Azure;
using MediatR;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Features.Orders.Commands.Models
{
    public class CheckoutOrderCommandModel : IRequest<Session>
    {
        public Guid OrderId { get; set; }
        public string SuccessUrl { get; set; }
        public string FailedUrl { get; set; }
        public string UserId { get; set; }
    }
}
