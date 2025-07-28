using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
    public class BookImages
    {
        public Guid BookId { get; set; }
        public Guid ImageId { get; set; }
        public BookBase Book { get; set; }
        public Image Image { get; set; }
    }
}
