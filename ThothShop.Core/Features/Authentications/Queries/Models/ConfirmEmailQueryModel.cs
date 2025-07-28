using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Authentications.Queries.Models
{
    public class ConfirmEmailQueryModel : IRequest<Response<string>>
    {
        public string? email { get; set; }
        public string? code { get; set; }
    }
}
