using MediatR;
using System.ComponentModel.DataAnnotations;
using ThothShop.Core.Bases;
using ThothShop.Domain.Enums;

namespace ThothShop.Core.Features.Orders.Commands.Models
{
    public class UpdateOrderStatusCommandModel : IRequest<Response<string>>
    {
        public UpdateOrderStatusCommandModel(Guid id, OrderStatus newStatus)
        {
            Id = id;
            NewStatus = newStatus;
        }

        public Guid Id { get; set; }

        [Required]
        public OrderStatus NewStatus { get; set; }
    }
} 