using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Models
{
    public class Wishlist
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
