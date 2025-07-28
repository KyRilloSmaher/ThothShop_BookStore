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
    public class GetTopSellingBooksQueryModel: IRequest<Response<IEnumerable<GetBookResponseForDashboard>>>
    {
        //public int count { get; set; } = 10; // Default value
        public GetTopSellingBooksQueryModel(/*int count*/)
        {
            //this.count = count;
        }
    }
}
