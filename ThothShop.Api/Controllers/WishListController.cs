using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.UsedBooks.Queries.Models;
using ThothShop.Core.Features.WishLists.Commands.Models;
using ThothShop.Core.Features.WishLists.Queries.Models;
using ThothShop.Domain.AppMetaData;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    public class WishListController : ApplicationBaseController
    {
        public WishListController() { }

        [HttpGet(Router.WishList.GetMyWishList)]
        public async Task<IActionResult> GetMyWishList()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = await Mediator.Send(new GetMyWishListQueryModel(userId));
            return FinalResponse(response);
        }
        [HttpGet(Router.WishList.IsInWishList)]
        public async Task<IActionResult> IsInWishList([FromRoute] Guid bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = await Mediator.Send(new IsInWishListQueryModel(bookId, userId));
            return FinalResponse(response);
        }

        [HttpPost(Router.WishList.AddToWishList)]
        public async Task<IActionResult> AddToWishList([FromRoute] Guid bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = new AddToWishListCommandModel(bookId , userId);
            var response = await Mediator.Send(model);
            return FinalResponse(response);
        }

        [HttpDelete(Router.WishList.RemoveFromWishList)]
        public async Task<IActionResult> RemoveFromWishList([FromRoute] Guid bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = await Mediator.Send(new RemoveFromWishListCommandModel(bookId, userId));
            return FinalResponse(response);
        }

        [HttpDelete(Router.WishList.ClearWishList)]
        public async Task<IActionResult> ClearUserWishList()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = await Mediator.Send(new ClearWishlistCommnandModel(userId));
            return FinalResponse(response);
        }
    }
} 