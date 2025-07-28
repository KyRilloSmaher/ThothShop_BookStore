using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Authors.Queries.Models
{
    public class GetAuthorsCountQueryModel:IRequest<Response<int>>
    {
    }
}
