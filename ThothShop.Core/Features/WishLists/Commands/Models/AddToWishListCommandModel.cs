using MediatR;
using System.ComponentModel.DataAnnotations;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.WishLists.Commands.Models
{
    public class AddToWishListCommandModel : IRequest<Response<bool>>
    {
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public string UserId { get; set; }

        public AddToWishListCommandModel(Guid bookId, string userId)
        {
            BookId = bookId;
            UserId = userId;
        }
    }
} 