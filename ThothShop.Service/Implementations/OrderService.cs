using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Service.Contract;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using Microsoft.IdentityModel.Tokens;
using ThothShop.Service.Commans;
using ThothShop.Infrastructure.Implementations;

namespace ThothShop.Service.implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Order?> CreateOrderFromCartIdAsync(Guid cartId)
        {
            var result = await _orderRepository.CreateOrderFromCart(cartId);
            if (result == null)
                throw new InvalidOperationException($"Failed to create order from cart with ID {cartId}");
            return result;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id , bool tracked =false)
        {
            var order = await _orderRepository.GetOrderByIdWithDetailsAsync(id,false);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found");

            return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
        {
            if (userId.IsNullOrEmpty())
                throw new ArgumentException("User ID must be specified");

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found");

            return await _orderRepository.GetUserOrdersAsync(userId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _orderRepository.GetOrdersByStatusAsync(status);
        }

        public async Task<bool> UpdateOrderStatusToCompletedAsync(Guid id, OrderStatus newStatus ,int transictionId)
        {
            var order = await _orderRepository.GetOrderByIdWithDetailsAsync(id,true);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found");

            // Validate status transition
            if (!IsValidStatusTransition(order.Status, newStatus))
                throw new InvalidOperationException($"Invalid status transition from {order.Status} to {newStatus}");

            order.Status = newStatus;
            order.PaymentId = transictionId;
            await _orderRepository.UpdateAsync(order);
            return true;
        }
        public async Task<bool> UpdateOrderStatusAsync(Guid id, OrderStatus newStatus)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found");

            // Validate status transition
            if (!IsValidStatusTransition(order.Status, newStatus))
                throw new InvalidOperationException($"Invalid status transition from {order.Status} to {newStatus}");

            order.Status = newStatus;

            await _orderRepository.UpdateAsync(order);
            return true;
        }

        //public async Task<SalesReport> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        //{
        //    if (startDate > endDate)
        //        throw new ArgumentException("Start date must be before end date");

        //    var orders = await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);

        //    var report = new SalesReport
        //    {
        //        StartDate = startDate,
        //        EndDate = endDate,
        //        TotalOrders = orders.Count(),
        //        TotalRevenue = orders.Sum(o => o.TotalAmount),
        //        OrdersByStatus = orders.GroupBy(o => o.Status)
        //            .ToDictionary(g => g.Key, g => g.Count()),
        //        PopularItems = orders.SelectMany(o => o.Items)
        //            .GroupBy(i => i.BookId)
        //            .OrderByDescending(g => g.Sum(i => i.Quantity))
        //            .Take(5)
        //            .ToDictionary(g => g.Key, g => g.Sum(i => i.Quantity))
        //    };

        //    return report;
        //}

        //public async Task<bool> ProcessPaymentAsync(Guid orderId, PaymentInfoDto paymentInfo)
        //{
        //    if (paymentInfo == null)
        //        throw new ArgumentNullException(nameof(paymentInfo));

        //    var order = await _orderRepository.GetByIdAsync(orderId);
        //    if (order == null)
        //        throw new KeyNotFoundException($"Order with ID {orderId} not found");

        //    if (order.Status != OrderStatus.Pending)
        //        throw new InvalidOperationException($"Cannot process payment for order in {order.Status} status");

        //    // Process payment through payment service
        //    var paymentResult = await _paymentService.ProcessPaymentAsync(
        //        order.TotalAmount,
        //        paymentInfo);

        //    if (paymentResult.Success)
        //    {
        //        order.Status = OrderStatus.Processing;
        //        order.PaymentDate = DateTime.UtcNow;
        //        order.TransactionId = paymentResult.TransactionId;
        //        await _orderRepository.UpdateAsync(order);
        //        return true;
        //    }

        //    order.Status = OrderStatus.PaymentFailed;
        //    await _orderRepository.UpdateAsync(order);
        //    return false;
        //}

        private bool IsValidStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            switch (currentStatus)
            {
                case OrderStatus.Pending:
                    return newStatus == OrderStatus.Processing ||
                           newStatus == OrderStatus.Cancelled;
                case OrderStatus.Processing:
                    return newStatus == OrderStatus.Shipped ||
                           newStatus == OrderStatus.Cancelled;
                case OrderStatus.Shipped:
                    return newStatus == OrderStatus.Completed ||
                           newStatus == OrderStatus.Returned;
                case OrderStatus.PaymentFailed:
                    return newStatus == OrderStatus.Pending ||
                           newStatus == OrderStatus.Cancelled;
                case OrderStatus.Cancelled:
                case OrderStatus.Completed:
                case OrderStatus.Returned:
                    return false; // These are final states
                default:
                    return false;
            }
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
           var order = await _orderRepository.GetOrderByIdWithDetailsAsync(id,true);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found");

           await _orderRepository.DeleteAsync(order);
            return true;
        }

        public async Task<IEnumerable<Order>> GetRecentOrders()
        {
            return await _orderRepository.GetRecentOrdersAsync();
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            return await _orderRepository.GetTotalSalesAsync();
        }

        public async Task<int> GetOrderCountAsync()
        {
            return await _orderRepository.GetTotalNumberOfOrders();
        }

        public IQueryable<Order> GetAllOrdersAsync()
        {
            return _orderRepository.GetAllOrders();
        }
    }

  
}