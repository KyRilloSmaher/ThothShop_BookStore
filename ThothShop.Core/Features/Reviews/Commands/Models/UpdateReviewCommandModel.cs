using MediatR;
using System.ComponentModel.DataAnnotations;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Reviews.Commands.Models
{
    public class UpdateReviewCommandModel : IRequest<Response<string>>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }
    }
} 