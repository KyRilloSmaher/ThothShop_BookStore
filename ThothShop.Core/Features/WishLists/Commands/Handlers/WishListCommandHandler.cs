using AutoMapper;
using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.WishLists.Commands.Models;
using ThothShop.Core.Features.WishLists.Queries.Responses;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.WishLists.Commands.Handlers
{
    public class WishlistCommandHandler : ResponseHandler,
        IRequestHandler<AddToWishListCommandModel, Response<bool>>,
        IRequestHandler<RemoveFromWishListCommandModel, Response<string>>,
        IRequestHandler<ClearWishlistCommnandModel, Response<string>>
    {
        private readonly IWishListService _wishlistService;
        private readonly IMapper _mapper;

        public WishlistCommandHandler(IWishListService wishlistService, IMapper mapper)
        {
            _wishlistService = wishlistService;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(AddToWishListCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _wishlistService.AddToWishListAsync(request.BookId, request.UserId);
            if (!result)
            {
                return BadRequest<bool>("Failed to add Book to wishlist");
            }

            return Success(true,"Book added to wishlist successfully");
        }

        public async Task<Response<string>> Handle(RemoveFromWishListCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _wishlistService.RemoveFromWishListAsync( request.BookId, request.UserId);
            if (!result)
            {
                return NotFound<string>("Book not found in wishlist");
            }

            return Success("Book removed from wishlist successfully");
        }

        public async Task<Response<string>> Handle(ClearWishlistCommnandModel request, CancellationToken cancellationToken)
        {
           await _wishlistService.ClearWishListAsync(request.UserId);

            return Success("Wishlist cleared successfully");
        }
    }
} 