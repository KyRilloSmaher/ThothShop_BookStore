using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Service.Commans;

namespace ThothShop.Core.Features.Authentications.Commands.Models
{
    public class RefreshTokenCommandModel : IRequest<Response<JwtResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
