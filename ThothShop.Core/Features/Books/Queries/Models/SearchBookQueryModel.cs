using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Books.Queries.Responses;
using ThothShop.Service.Commans;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class SearchBookQueryModel :FilterListParams,IRequest<Response<IEnumerable<GetBookResponse>>>
    {
        public SearchBookQueryModel()
        {
          
        }

  
    }
}
