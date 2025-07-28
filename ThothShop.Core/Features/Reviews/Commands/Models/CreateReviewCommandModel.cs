using MediatR;
using System.ComponentModel.DataAnnotations;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Reviews.Commands.Models
{
    public class CreateReviewCommandModel : IRequest<Response<string>>
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }
    }
} 