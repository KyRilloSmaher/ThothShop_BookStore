using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Carts.Commands.Models
{
    public class DeleteCartCommandModel : IRequest<Response<bool>>
    {
        public DeleteCartCommandModel(Guid cartId)
        {
            CartId = cartId;
        }

        public Guid CartId { get; set; }
    }
  
}
