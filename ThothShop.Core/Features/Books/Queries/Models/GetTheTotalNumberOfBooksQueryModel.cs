using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class GetTheTotalNumberOfBooksQueryModel : IRequest<Response<int>>
    {
    }
}
