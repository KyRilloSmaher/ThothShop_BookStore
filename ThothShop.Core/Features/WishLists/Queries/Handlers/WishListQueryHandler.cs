using AutoMapper;
using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.WishLists.Queries.Models;
using ThothShop.Core.Features.WishLists.Queries.Responses;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.WishLists.Queries.Handlers
{
    public class WishListQueryHandler : ResponseHandler,
        IRequestHandler<GetWishListByIdQueryModel, Response<WishlistItemResponse>>,
        IRequestHandler<GetMyWishListQueryModel, Response<WishlistResponse>>,
        IRequestHandler<IsInWishListQueryModel, Response<bool>>
    {
        private readonly IWishListService _wishListService;
        private readonly IMapper _mapper;

        public WishListQueryHandler(IWishListService wishListService, IMapper mapper)
        {
            _wishListService = wishListService;
            _mapper = mapper;
        }

        public async Task<Response<WishlistItemResponse>> Handle(GetWishListByIdQueryModel request, CancellationToken cancellationToken)
        {
            var wishlist = await _wishListService.GetByIdAsync(request.Id);
            if (wishlist == null)
                return NotFound<WishlistItemResponse>("Wishlist item not found");
            var wishlistItem = _mapper.Map<WishlistItemResponse>(wishlist);
            if (wishlist == null)
                return NotFound<WishlistItemResponse>("Wishlist item not found");

            return Success(wishlistItem);
        }

        public async Task<Response<WishlistResponse>> Handle(GetMyWishListQueryModel request, CancellationToken cancellationToken)
        {
            var wishlist = await _wishListService.GetUserWishListAsync(request.UserId);
            if (wishlist == null)
                return NotFound<WishlistResponse>("Wishlist not found");
            var wishlistitemsResponse = _mapper.Map<IEnumerable<WishlistItemResponse>>(wishlist);
            var wishlistResponse = new WishlistResponse
            {
                UserId = request.UserId,
                Items = wishlistitemsResponse,
                TotalItems = wishlistitemsResponse.Count()
            };
            return Success(wishlistResponse , message :"User WishList Retrived Successfuly");
        }
        public async Task<Response<bool>> Handle(IsInWishListQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _wishListService.IsInWishListAsync(request.BookId, request.UserId);
            if (!result)
                return NotFound<bool>("Book not found in Your wishlist");

            return Success(result,"Book found in Your wishlist");
        }
    }
} 