using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.WishLists.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.WishLists.Queries.Models
{
    public class GetWishListByIdQueryModel : IRequest<Response<WishlistItemResponse>>
    {
        public Guid Id { get; private set; }

        public GetWishListByIdQueryModel(Guid id)
        {
            Id = id;
        }
    }
} 