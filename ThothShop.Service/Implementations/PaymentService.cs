using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.RepositoriesContracts;
using ThothShop.Service.Contract;

namespace ThothShop.Service.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            await _paymentRepository.AddAsync(payment);

            return payment;
        }
        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await _paymentRepository.GetPaymentByIdAsync(paymentId);
        }

        public async Task<Payment?> GetPaymentByOrderIdAsync(Guid orderId)
        {
            return await _paymentRepository.GetPaymentsByOrderIdAsync(orderId);
        }
        public async Task<Payment?> UpdatePaymentStatusAsync(int paymentId, PaymentStatus newStatus, string? transactionId = null)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId,true);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {paymentId} not found");

            payment.Status = newStatus;
            if (!string.IsNullOrEmpty(transactionId))
                payment.TransactionId = transactionId;

            await _paymentRepository.UpdateAsync(payment);

            return payment;
        }



        public async Task<IEnumerable<Payment>> GetUserPaymentsAsync(string userId)
        {
            return await _paymentRepository.GetUserPaymentsAsync(userId);
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _paymentRepository.GetTotalRevenueAsync(startDate, endDate);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(PaymentStatus status)
        {
            return await _paymentRepository.GetPaymentsByStatusAsync(status);
        }

        public async Task<bool> ValidatePaymentAsync(int paymentId)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
            if (payment == null)
                return false;

            // Here you would implement validation logic specific to your payment processor
            // This could include checking transaction status, verifying amounts, etc.
            return payment.Status == PaymentStatus.Completed;
        }



    }

}


