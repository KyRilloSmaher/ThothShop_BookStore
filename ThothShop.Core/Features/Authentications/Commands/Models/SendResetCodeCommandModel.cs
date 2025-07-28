using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Authentications.Commands.Models
{
    public class SendResetCodeCommandModel : IRequest<Response<string>>
    {
        [Required(ErrorMessage ="Email Is Required here , please Add a Valid Email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
