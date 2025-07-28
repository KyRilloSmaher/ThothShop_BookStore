using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.WishLists.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.WishLists.Queries.Models
{
    public class GetMyWishListQueryModel : IRequest<Response<WishlistResponse>>
    {
        public string UserId { get; set; }

        public GetMyWishListQueryModel(string userId)
        {
            UserId = userId;
        }
    }
} 