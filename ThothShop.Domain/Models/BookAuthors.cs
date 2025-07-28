using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
    public class BookAuthors
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
        public BookBase Book { get; set; }
        public Author Author { get; set; }
    }
}
