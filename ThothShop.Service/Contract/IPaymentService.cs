using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Domain.Enums;

namespace ThothShop.Service.Contract
{
    public interface IPaymentService
    {
        Task<Payment?> CreatePaymentAsync(Payment payment);
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task<Payment?> GetPaymentByOrderIdAsync(Guid orderId);
        Task<Payment?> UpdatePaymentStatusAsync(int paymentId, PaymentStatus newStatus, string? transactionId = null);
        Task<IEnumerable<Payment>> GetUserPaymentsAsync(string userId);
        Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(PaymentStatus status);
        Task<bool> ValidatePaymentAsync(int paymentId);
    }
}
