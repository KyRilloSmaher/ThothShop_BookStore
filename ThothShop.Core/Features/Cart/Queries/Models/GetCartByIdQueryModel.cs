using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Carts.Queries.Responses;

namespace ThothShop.Core.Features.Carts.Queries.Models
{
    public class GetCartByIdQueryModel: IRequest<Response<Cartresponse>>
    {
        public GetCartByIdQueryModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
   
}
