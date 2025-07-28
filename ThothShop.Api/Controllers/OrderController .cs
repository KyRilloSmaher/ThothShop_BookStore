using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.Books.Queries.Models;
using ThothShop.Core.Features.Orders.Commands.Models;
using ThothShop.Core.Features.Orders.Queries.Models;
using ThothShop.Domain.AppMetaData;
using ThothShop.Domain.Enums;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class OrderController : ApplicationBaseController
    {
        public OrderController() { }

        [HttpGet(Router.Order.GetById)]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid Id)
        {
            var query = new GetOrderByIdQueryModel(Id);
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }

        [HttpGet(Router.Order.GetUserOrders)]
        public async Task<IActionResult> GetUserOrders()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID not found in claims");

            var query = new GetUserOrdersQueryModel(userId);
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }

        [HttpGet(Router.Order.GetByStatus)]
        public async Task<IActionResult> GetOrdersByStatus([FromRoute] OrderStatus status)
        {
            var query = new GetOrdersByStatusQueryModel(status);
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }
        [HttpGet(Router.Order.GetAll)]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrderQueryModel model)
        {
            var response = await Mediator.Send(model);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Order.TotalSales)]
        public async Task<IActionResult> GetTotalSales()
        {
            var query = new GetTotalSalesQueryModel();
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }

        [HttpGet(Router.Order.OrdersCount)]
        public async Task<IActionResult> GetOrdersCount()
        {
            var query = new GetOrderCountQueryModel();
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }
        [HttpGet(Router.Order.RecentOrders)]
        public async Task<IActionResult> GetRecentOrders()
        {
            var query = new GetRecentOrdersQueryModel();
            var result = await Mediator.Send(query);
            return FinalResponse(result);
        }
        [HttpPost(Router.Order.CreateFromCart)]
        public async Task<IActionResult> CreateOrderFromCart([FromRoute] Guid cartId)
        {
            var command = new CreateOrderFromCartCommandModel(cartId);
            var result = await Mediator.Send(command);
            return FinalResponse(result);
        }

        [HttpPut(Router.Order.UpdateStatus)]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] Guid Id, [FromForm] OrderStatus newStatus)
        {
            var command = new UpdateOrderStatusCommandModel(Id, newStatus);
            var result = await Mediator.Send(command);
            return FinalResponse(result);
        }

        [HttpDelete(Router.Order.Delete)]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            var command = new DeleteOrderCommandModel(id);
            var result = await Mediator.Send(command);
            return FinalResponse(result);
        }
    }
}
