using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
    public class AuthorCategories
    {
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }
    }
}
