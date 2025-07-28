using MediatR;
using System.ComponentModel.DataAnnotations;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Carts.Commands.Models
{
    public class AddToCartCommandModel : IRequest<Response<string>>
    {
        [Required]
        public Guid CartId { get; set; } 
        [Required]
        public Guid BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
        [Required]
        public decimal UnitPrice { get; set; }

    }
} 