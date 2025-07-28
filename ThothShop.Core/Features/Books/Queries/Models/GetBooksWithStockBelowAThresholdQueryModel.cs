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
    public class GetBooksWithStockBelowAThresholdQueryModel : IRequest<Response<IEnumerable<GetBookResponse>>>
    {
        public int Threshold { get; set; }
        public GetBooksWithStockBelowAThresholdQueryModel(int threshold )
        {
            Threshold = threshold;
        }
    }
}
