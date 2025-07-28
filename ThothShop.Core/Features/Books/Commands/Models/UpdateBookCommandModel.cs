using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Books.Commands.Models
{
    public class UpdateBookCommandModel : IRequest<Response<string>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string CategoryId { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        public int Stock { get; set; }
        public IFormFile? PrimaryImage { get; set; } = null!;
        public List<IFormFile>? Images { get; set; }
    }
}
