using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;

namespace ThothShop.Service.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderFromCartIdAsync(Guid cartId);
        Task<Order?> GetOrderByIdAsync(Guid id,bool tracked = false);
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        IQueryable<Order> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatusToCompletedAsync(Guid id, OrderStatus newStatus , int TransictionId);
        Task<bool> UpdateOrderStatusAsync(Guid id, OrderStatus newStatus);
        Task<bool> DeleteOrderAsync(Guid id);
        Task<IEnumerable<Order>>GetRecentOrders();
        Task<decimal> GetTotalSalesAsync();
        Task<int> GetOrderCountAsync();
        //Task<SalesReport> GetSalesReportAsync(DateTime startDate, DateTime endDate);
        //Task<bool> ProcessPaymentAsync(Guid orderId, PaymentInfoDto paymentInfo);
    }
}
