using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Service.Contract
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string message);
        Task<bool> SendConfirmationEmailAsync(string email, string callbackUrl);
        Task<bool> SendPasswordResetEmailAsync(string email, string subject, string message);
        Task<bool> SendOrderConfirmationAsync(string userId, Guid orderId);

    }
}
