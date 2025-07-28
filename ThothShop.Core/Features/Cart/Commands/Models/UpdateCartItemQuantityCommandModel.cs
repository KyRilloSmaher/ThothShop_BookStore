using MediatR;
using System.ComponentModel.DataAnnotations;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Carts.Commands.Models
{
    public class UpdateCartItemQuantityCommandModel : IRequest<Response<string>>
    {
        [Required]
        public Guid CartId { get; set; }
        [Required]
        public Guid BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int NewQuantity { get; set; }
    }
} 