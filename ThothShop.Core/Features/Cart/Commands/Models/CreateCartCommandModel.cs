using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Carts.Commands.Models
{
    public class CreateCartCommandModel : IRequest<Response<Guid>>
    {
        public CreateCartCommandModel(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set;}
    }
}
