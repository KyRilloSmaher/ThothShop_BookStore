using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Authors.Queries.Models.Base;
using ThothShop.Core.Features.Orders.Queries.Responses;
using ThothShop.Service.Wrappers;

namespace ThothShop.Core.Features.Orders.Queries.Models
{
    public class GetAllOrderQueryModel : PagintedRequest,IRequest<PaginatedResult<OrderResponse>>
    {
    }
}
