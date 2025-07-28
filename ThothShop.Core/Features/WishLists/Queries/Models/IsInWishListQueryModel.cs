using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.WishLists.Queries.Models
{
    public class IsInWishListQueryModel : IRequest<Response<bool>>
    {
        public IsInWishListQueryModel(Guid bookId, string userId)
        {
            BookId = bookId;
            UserId = userId;
        }

        public Guid BookId { get; set; }
        public string UserId { get; set; }
    }
}
