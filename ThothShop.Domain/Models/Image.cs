using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }

        public ICollection<BookImages> BookImages { get; set; }
        public ICollection<AuthorImages> authorIamges { get; set; }
        public Category Category { get; set; }
    }
}
