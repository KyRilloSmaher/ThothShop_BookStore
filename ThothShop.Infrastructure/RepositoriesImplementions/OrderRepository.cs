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
using ThothShop.Infrastructure.Contracts;

namespace ThothShop.Infrastructure.Implementions
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderRepository(ApplicationDBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Order?> CreateOrderFromCart(Guid cartId)
        {
            var cart = await _context.Carts.Include(c => c.Items).ThenInclude(ci => ci.Book).FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart == null || !cart.Items.Any())
            {
                return null;
            }
            var items = cart.Items.Select(ci => new OrderItem
            {
                BookId = ci.BookId,
                Quantity = ci.Quantity,
                UnitPrice = ci.Book.Price
            }).ToList();
            var order = new Order
            {
                UserId = cart.UserId,
                Status = OrderStatus.Pending,
                Items = items
            };
             order.CalculateTotalAmount();
             await AddAsync(order);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(Order order)
        {
            if (order == null)
            {
                return false;
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.User).Include(o=>o.Items).AsNoTracking().AsQueryable();
        }

        public async Task<Order?> GetOrderByIdWithDetailsAsync(Guid orderId, bool tracked =false)
        {
            return tracked ?
                      await _context.Orders.Include(o => o.Items).Include(o => o.User).AsTracking().FirstOrDefaultAsync(o => o.Id == orderId) :
                      await _context.Orders.Include(o => o.Items).Include(o => o.User).AsNoTracking().FirstOrDefaultAsync(o => o.Id == orderId);

        }

        public async Task<int> GetOrderCountByStatusAsync(OrderStatus status)
        {
            return await _context.Orders
             .CountAsync(o => o.Status == status);
        }
        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _context.Orders
                .Where(o => o.Status == status)
                .Include(o => o.User)
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int count = 10)
        {
            return await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .Include(o=>o.Items)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetTotalNumberOfOrders()
        {
            return await (_context.Orders.CountAsync());    
        }

        public async  Task<decimal> GetTotalSalesAsync(DateTime startDate= default, DateTime endDate = default)

        {
            if (startDate == default || endDate == default)
            {
               return await _context.Orders
                  .SumAsync(o => o.TotalPrice);
            }
            return await _context.Orders
                  .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
                  .SumAsync(o => o.TotalPrice);
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> SearchOrdersAsync(string searchTerm)
        {
            return await _context.Orders.Include(o => o.User)
                                         .Where(o => EF.Functions.Like(o.User.Email, $"%{searchTerm}%") || EF.Functions.Like(o.User.UserName, $"%{searchTerm}%"))
                                        .AsNoTracking()
                                        .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus)
        {
           var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return false;
            }
            order.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
