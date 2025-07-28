using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Categories.Commands.Models
{
    public class UpdateCategoryCommandModel :IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? Icon { get; set; }

    }
}
