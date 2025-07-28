using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;

namespace ThothShop.Domain.Models
{
    public class Book : BookBase
    {
        public int Stock { get; set; }
        public ICollection<Review> Reviews { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection <Wishlist> Wishlist { get; set; } 

        // Constructor
        public Book()
        {
            BookImages = new HashSet<BookImages>();
            Authors = new HashSet<BookAuthors>();
            Reviews = new HashSet<Review>();
            OrderItems = new HashSet<OrderItem>();
            Wishlist = new HashSet<Wishlist>();
            CartItems = new HashSet<CartItem>();
        }
    }
}
