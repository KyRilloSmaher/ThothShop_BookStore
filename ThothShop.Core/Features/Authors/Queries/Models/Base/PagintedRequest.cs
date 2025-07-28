using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Features.Authors.Queries.Models.Base
{
    public class PagintedRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
