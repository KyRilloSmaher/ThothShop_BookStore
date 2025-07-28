using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Orders.Queries.Models
{
    public class GetOrderCountQueryModel:IRequest<Response<int>>
    {
    }
}
