using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.Cart.Queries.Models;
using ThothShop.Core.Features.Carts.Commands.Models;
using ThothShop.Core.Features.Carts.Queries.Models;
using ThothShop.Domain.AppMetaData;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    public class CartController : ApplicationBaseController
    {
        public CartController() { }
        [HttpGet(Router.Cart.GetCartById)]
        public async Task<IActionResult> GetCartById([FromRoute] Guid Id)
        {
            var query = new GetCartByIdQueryModel(Id);
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }

        [HttpGet(Router.Cart.GetUserCarts)]
        public async Task<IActionResult> GetCartsByUserId()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID not found in claims");
            var query = new GetUserCartsQueryModel(userId);
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }

        [HttpGet(Router.Cart.GetAllCarts)]
        public async Task<IActionResult> GetAllCarts()
        {
            var query = new GetAllCartsQuery();
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }
        [HttpGet(Router.Cart.GetItemCount)]
        public async Task<IActionResult> GetCartItemCount([FromRoute] Guid Id)
        {
            var query = new GetCartItemCountQueryModel(Id);
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }
        [HttpGet(Router.Cart.GetTotal)]
        public async Task<IActionResult> GetCartTotal([FromRoute] Guid Id)
        {
            var query = new GetCartTotalQueryModel(Id);
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }
        [HttpGet(Router.Cart.GetCartItems)]
        public async Task<IActionResult> GetCartItems([FromRoute] Guid Id)
        {
            var query = new GetCartItemsQueryModel(Id);
            var result = await Mediator.Send(query);
            return FinalResponse(result);

        }
        [HttpPost(Router.Cart.CreateCart)]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartCommandModel command)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            command.UserId = userId;
            var result = await Mediator.Send(command);
            return FinalResponse(result);
        }
        [HttpPost(Router.Cart.AddToCart)]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommandModel command)
        {
            var result = await Mediator.Send(command);
            return FinalResponse(result);
        }

        [HttpDelete(Router.Cart.DeleteCart)]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid Id)
        {
            var command = new DeleteCartCommandModel(Id);
            var result = await Mediator.Send(command);
            return FinalResponse(result);
        }

    }
}
