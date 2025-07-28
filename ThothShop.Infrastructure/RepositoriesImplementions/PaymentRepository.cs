using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.RepositoriesContracts;

namespace ThothShop.Infrastructure.RepositoriesImplementions
{
    public class PaymentRepository :GenericRepository<Payment>, IPaymentRepository
    {
        private readonly ApplicationDBContext _context;
        public PaymentRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreatePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeletePaymentAsync(Payment payment)
        {
            _context.Payments.Remove(payment);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId, bool tracked = false)
        {
            return tracked
                ? await _context.Payments.FirstOrDefaultAsync(p=>p.Id ==paymentId)
                : await _context.Payments.AsNoTracking().FirstOrDefaultAsync(p => p.Id == paymentId);
        }
        public async Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Payments
                .Where(p => p.Status == PaymentStatus.Completed);

            if (startDate.HasValue)
                query = query.Where(p => p.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.CreatedAt <= endDate.Value);

            return await query.SumAsync(p => p.Amount);
        }
        public async Task<Payment?> GetPaymentsByOrderIdAsync(Guid orderId)
        {
            return await _context.Payments.AsNoTracking()
                .FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(PaymentStatus status)
        {
            return await _context.Payments
                .Where(p => p.Status == status)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Payment>> GetUserPaymentsAsync(string userId)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .Where(p => p.Order.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
        public async Task<bool> UpdatePaymentStatusAsync(int paymentId, PaymentStatus newStatus)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            payment.Status = newStatus;
            _context.Payments.Update(payment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
