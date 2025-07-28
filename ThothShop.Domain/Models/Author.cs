using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThothShop.Domain.Enums;

namespace ThothShop.Domain.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public int ViewCount { get; set; }
        public Gender Gender { get; set; }
        public Nationality Nationality { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public ICollection<BookAuthors> bookAuthors{ get; set; } = new HashSet<BookAuthors>();
        public ICollection<AuthorImages> AuthorImages { get; set; } = new HashSet<AuthorImages>();
        public ICollection<AuthorCategories> authorCategories { get; set; } = new HashSet<AuthorCategories>();

        // Constructor
        public Author()
        {
            bookAuthors = new HashSet<BookAuthors>();
            authorCategories = new HashSet<AuthorCategories>();
        }
    }
}
