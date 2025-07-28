using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.WishLists.Commands.Models
{
    public class RemoveFromWishListCommandModel : IRequest<Response<string>>
    {
        public Guid BookId { get; set; }
        public string UserId { get; set; }

        public RemoveFromWishListCommandModel(Guid bookId, string userId)
        {
            BookId = bookId;
            UserId = userId;
        }
    }
}
