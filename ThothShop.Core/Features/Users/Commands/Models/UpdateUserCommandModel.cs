using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Users.Commands.Models
{
    public class UpdateUserCommandModel: IRequest<Response<string>>
    {
        [Required(ErrorMessage ="Id is Required .")]
        public string Id { get; set; }
        [Required(ErrorMessage = "UserName is Required .")]
        [StringLength(50, ErrorMessage = "UserName must be less than 50 characters.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Phone Number is Required .")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Phone number must be 11 Number .")]
        public string PhoneNumber { get; set; }
        [StringLength(100, ErrorMessage = "Address must be less than 100 characters.")]
        public string Address { get; set; }
    }
}
