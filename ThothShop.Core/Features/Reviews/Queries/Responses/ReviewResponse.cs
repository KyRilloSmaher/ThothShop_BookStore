using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Features.Reviews.Queries.Responses
{
    public class ReviewResponse
    {
        public Guid Id { get; set; }
        public string UserName{ get; set; }
        public string BookName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
