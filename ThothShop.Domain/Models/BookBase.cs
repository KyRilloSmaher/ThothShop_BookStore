using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
    abstract public class BookBase
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public int ViewCount { get;  set; }
        public DateTime PublishedDate { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public Category Category { get; set; }
        public ICollection<BookImages> BookImages { get; set; }
        public ICollection<BookAuthors> Authors { get; set; }
    }
}
