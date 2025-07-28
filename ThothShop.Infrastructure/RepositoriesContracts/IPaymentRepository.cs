using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.RepositoriesContracts
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Payment?> GetPaymentByIdAsync(int paymentId, bool tracked = false);
        Task<Payment?> GetPaymentsByOrderIdAsync(Guid orderId);
        Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(PaymentStatus status);
        Task<IEnumerable<Payment>> GetUserPaymentsAsync(string userId);
        Task<bool> UpdatePaymentStatusAsync(int paymentId, PaymentStatus newStatus);
        Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<bool> DeletePaymentAsync(Payment payment);
        Task<bool> CreatePaymentAsync(Payment payment);
    }

}
