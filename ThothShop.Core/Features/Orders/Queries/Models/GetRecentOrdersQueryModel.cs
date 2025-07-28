using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Orders.Queries.Responses;

namespace ThothShop.Core.Features.Orders.Queries.Models
{
    public class GetRecentOrdersQueryModel:IRequest<Response<IEnumerable<OrderResponse>>>
    {
    }
}
