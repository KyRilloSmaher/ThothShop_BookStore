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

namespace ThothShop.Core.Features.Authors.Commands.Models
{
    public class CreateAuthorCommandModel:IRequest<Response<string>>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bio is required")]
        public string Bio { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Nationality is Required")]
        public Nationality Nationality { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateOnly DateOfBirth { get; set; }

        public IFormFile PrimaryImage { get; set; }
        public IList<IFormFile> Images { get; set; }
    }
}
