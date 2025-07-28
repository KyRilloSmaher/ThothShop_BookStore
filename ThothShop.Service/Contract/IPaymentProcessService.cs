using Stripe.Checkout;
using ThothShop.Domain.Models;

namespace ThothShop.Service.Contract
{
    public interface IPaymentProcessService
    {
        Task<Session> ProcessPayment(Order order, string successurl, string failedurl);
    }
}
