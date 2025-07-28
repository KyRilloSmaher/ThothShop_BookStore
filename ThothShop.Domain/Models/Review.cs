using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ThothShop.Domain.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid BookId { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
