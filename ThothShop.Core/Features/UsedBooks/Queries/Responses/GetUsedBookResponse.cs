using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.UsedBooks.Queries.Responses
{
    public class GetUsedBookResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CategoryName { get; set; }
        public string primaryImage { get; set; }
        public ICollection<string> BookImages { get; set; }
        public string UserName { get; set; }
        public UsedBookCondition Condition { get; set; }
        public string Comment { get; set; }
        public string Note_WayOfConnect { get; set; }
    }
}
