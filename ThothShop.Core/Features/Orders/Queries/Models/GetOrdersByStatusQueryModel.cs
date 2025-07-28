using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Orders.Queries.Responses;
using ThothShop.Domain.Enums;

namespace ThothShop.Core.Features.Orders.Queries.Models
{
    public class GetOrdersByStatusQueryModel : IRequest<Response<IEnumerable<OrderResponse>>>
    {
        public GetOrdersByStatusQueryModel(OrderStatus status)
        {
            Status = status;
        }

        public OrderStatus Status { get; set; }
    }
} 