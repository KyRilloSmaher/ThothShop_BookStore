using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Orders.Queries.Responses;

namespace ThothShop.Core.Features.Orders.Queries.Models
{
    public class GetUserOrdersQueryModel : IRequest<Response<IEnumerable<OrderResponse>>>
    {
        public GetUserOrdersQueryModel(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
} 