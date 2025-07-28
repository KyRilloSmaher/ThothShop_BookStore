using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Orders.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.Orders.Queries.Models
{
    public class GetOrderByIdQueryModel : IRequest<Response<OrderResponse>>
    {
        public GetOrderByIdQueryModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
} 