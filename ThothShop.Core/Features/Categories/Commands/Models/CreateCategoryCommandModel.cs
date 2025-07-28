using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Categories.Commands.Models
{
    public class CreateCategoryCommandModel:IRequest<Response<string>>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        public IFormFile Icon { get; set; }
    }
}
