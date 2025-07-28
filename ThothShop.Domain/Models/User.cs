

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThothShop.Domain.Models
{
    public class User : IdentityUser
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? Code { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<UsedBook> usedBooks { get; set; } = new HashSet<UsedBook>();
        public ICollection<Cart> carts { get; set; } = new HashSet<Cart>();
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new HashSet<UserRefreshToken>();


    }
}