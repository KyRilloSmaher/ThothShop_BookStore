using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Service.Implementations
{
    public class PaymentProcessService : IPaymentProcessService
    {
        public async Task<Session> ProcessPayment(Order order, string successurl , string failedurl)
        {
            // Create a payment flow from the items in the cart.
            // Gets sent to Stripe API.
            var options = new SessionCreateOptions
            {
                // Stripe calls the URLs below when certain checkout events happen such as success and failure.
                SuccessUrl = successurl+$"&orderId={order.Id}", // Customer paid.
                CancelUrl = failedurl,  // Checkout cancelled.
                PaymentMethodTypes = new List<string> // Only card available in test mode?
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(order.TotalPrice * 100),
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Order:{order.Id}",
                     
                        },
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment" // One-time payment.
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return (session);
        }

       
    }
}
