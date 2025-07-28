using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Books.Queries.Responses;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class GetNewlyReleasedBooksQueryModel : IRequest<Response<IEnumerable<GetBookResponse>>>
    {
       /* public int count { get; set; } = 10; // Default value*/
        public GetNewlyReleasedBooksQueryModel(/*int count*/)
        {
            /*this.count = count;*/
        }
    }
}
