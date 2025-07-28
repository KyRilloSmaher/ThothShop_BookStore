using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
    public class AuthorImages
    {
        public Guid AuthorId { get; set; }
        public Guid ImageId { get; set; }
        public Author author { get; set; }
        public Image Image { get; set; }
    }
}
