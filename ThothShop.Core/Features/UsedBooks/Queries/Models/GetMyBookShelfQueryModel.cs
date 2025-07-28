using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.UsedBooks.Queries.Responses;

namespace ThothShop.Core.Features.UsedBooks.Queries.Models
{
    public class GetMyBookShelfQueryModel : IRequest<Response<IEnumerable<GetUsedBookResponse>>>
    {
        public string UserId { get; set; }
        public GetMyBookShelfQueryModel(string userId)
        {
            UserId = userId;
        }
    }
}
