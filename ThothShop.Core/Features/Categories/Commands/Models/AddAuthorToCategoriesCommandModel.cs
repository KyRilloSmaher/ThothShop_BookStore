using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Categories.Commands.Models
{
    public class AddAuthorToCategoriesCommandModel :IRequest<Response<string>>
    {
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public List<Guid> CategoryIds { get; set; } = new List<Guid>();
    }
}
