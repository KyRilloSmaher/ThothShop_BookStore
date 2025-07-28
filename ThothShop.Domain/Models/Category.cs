using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;

namespace ThothShop.Domain.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid IconId { get; set; }
        public Image Icon { get; set; } 
        public ICollection<BookBase> Books { get; set; }
        public ICollection<AuthorCategories> authorCategories { get; set; }

        // Constructor
        public Category()
        {
            Books = new HashSet<BookBase>();
            authorCategories = new HashSet<AuthorCategories>();
        }
    }
}
