using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.UsedBooks.Commands.Models;
using ThothShop.Core.Features.UsedBooks.Queries.Models;
using ThothShop.Core.Features.UsedBooks.Queries.UsedBooksQueriesHandlers;
using ThothShop.Domain.AppMetaData;
using ThothShop.Domain.Enums;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class BookShelfController : ApplicationBaseController
    {
        [HttpGet(Router.userBookShelf.GetAllBookShelves)]
        public async Task<IActionResult> GetAllBookShelves()
        {
            var response = await Mediator.Send(new GetAllUsedBooksQueryModel());
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet(Router.userBookShelf.GetUsedBookDetailsById)]
        public async Task<IActionResult> GetUsedBookById([FromRoute] Guid Id)
        {
            var response = await Mediator.Send(new GetUsedBookByIdQueryModel(Id));
            return FinalResponse(response);
        }

        [HttpGet(Router.userBookShelf.GetMyBookShelf)]
        public async Task<IActionResult> GetMyBookShelf()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = await Mediator.Send(new GetMyBookShelfQueryModel(userId));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.userBookShelf.SearchUsedBooks)]
        public async Task<IActionResult> SearchUsedBooks([FromQuery] string searchString)
        {
            var response = await Mediator.Send(new SearchUsedBooksQueryModel(searchString));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.userBookShelf.FilterUsedBooksByCondition)]
        public async Task<IActionResult> FilterUsedBooksByCondition([FromQuery] UsedBookCondition condition)
        {
            var response = await Mediator.Send(new FilterUsedBooksByConditionQueryModel(condition));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPost(Router.userBookShelf.CreateUsedBook)]
        public async Task<IActionResult> CreateUsedBook([FromForm] CreateUsedBookCommandModel model)
        { 
            var response = await Mediator.Send(model);
            return FinalResponse(response);
        }
        [HttpPut(Router.userBookShelf.UpdateUsedBook)]
        public async Task<IActionResult> UpdateUsedBook([FromForm] UpdateUsedBookCommandModel model)
        {
            var response = await Mediator.Send(model);
            return FinalResponse(response);
        }
        [HttpDelete(Router.userBookShelf.DeleteUsedBook)]
        public async Task<IActionResult> DeleteUsedBook([FromRoute] Guid Id)
        {
            var response = await Mediator.Send(new DeleteUsedBookCommandModel(Id));
            return FinalResponse(response);
        }
    }
}
