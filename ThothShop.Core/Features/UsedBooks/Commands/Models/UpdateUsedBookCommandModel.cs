using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Domain.Enums;

namespace ThothShop.Core.Features.UsedBooks.Commands.Models
{
    public class UpdateUsedBookCommandModel :IRequest<Response<string>>
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime PublishedDate { get; set; }
      
        public IFormFile? PrimaryImage { get; set; } = null!;
        public List<IFormFile>? Images { get; set; }

        public string UserId { get; set; }

        public UsedBookCondition Condition { get; set; }
        public string? Comment { get; set; }
  
        public string Note_WayOfConnect { get; set; }
    }
}
