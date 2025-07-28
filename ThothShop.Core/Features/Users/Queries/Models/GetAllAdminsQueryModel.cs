using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Users.Queries.Responses;

namespace ThothShop.Core.Features.Users.Queries.Models
{
    public class GetAllAdminsQueryModel:IRequest<Response<IEnumerable<UserResponse>>>
    {
    }
}
