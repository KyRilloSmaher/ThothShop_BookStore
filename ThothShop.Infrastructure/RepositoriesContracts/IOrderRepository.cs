using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.Contracts
{
    public interface IOrderRepository : IGenericRepository<Order>
    {    
        Task<Order?> GetOrderByIdWithDetailsAsync(Guid orderId , bool tracked = false);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
        Task<decimal> GetTotalSalesAsync(DateTime startDate =default, DateTime endDate =default);
        Task<IEnumerable<Order>> GetRecentOrdersAsync(int count = 10);
        Task<int> GetOrderCountByStatusAsync(OrderStatus status);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus);
        Task<IEnumerable<Order>> SearchOrdersAsync(string searchTerm);
        Task<Order?> CreateOrderFromCart(Guid cartId);
        Task<bool> DeleteOrderAsync(Order order);
        Task<int> GetTotalNumberOfOrders();
        IQueryable<Order> GetAllOrders();
    }
}
