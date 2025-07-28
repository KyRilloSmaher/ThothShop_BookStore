using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.Books.Commands.Models;
using ThothShop.Core.Features.Books.Queries.Models;
using ThothShop.Domain.AppMetaData;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class BookController : ApplicationBaseController
    {
        public BookController() { }



        
        
        [HttpGet(Router.Books.GetAllBooks)]
        public async Task<IActionResult> GetAllBooks([FromQuery]GetAllBooksPagintedQueryModel model)
        {
            var response = await Mediator.Send(model);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetById)]
        public async Task<IActionResult> GetBookById([FromRoute] Guid Id)
        {
            var response = await Mediator.Send(new GetBookByIdQueryModel(Id));
            return FinalResponse(response);
        }

        [HttpGet(Router.Books.SearchBooks)]
        public async Task<IActionResult> SearchBooks([FromQuery] SearchBookQueryModel model)
        {
            var response = await Mediator.Send(model);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetTopRatedBooks)]
        public async Task<IActionResult> GetTopRatedBooks()
        {
            var response = await Mediator.Send(new GetTopRatedBooksPagintedQueryModel());
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet(Router.Books.GetTopSellingBooks)]
        public async Task<IActionResult> GetTopSellingBooks()
        {
            var response = await Mediator.Send(new GetTopSellingBooksQueryModel());
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet(Router.Books.GetSimilarBooks)]
        public async Task<IActionResult> GetSimilarBooks([FromRoute] Guid Id)
        {
            var response = await Mediator.Send(new GetSimilarBooksQueryModel(Id));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetnewlyReleaseBooks)]
        public async Task<IActionResult> GetNewReleases()
        {
            var response = await Mediator.Send(new GetNewlyReleasedBooksQueryModel());
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetTotalNumberofBooks)]
        public async Task<IActionResult> GetTotalNumberofBooks()
        {
            var response = await Mediator.Send(new GetTheTotalNumberOfBooksQueryModel());
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }


        [HttpGet(Router.Books.GetBooksByAuthorId)]
        public async Task<IActionResult> GetBooksByAuthorId([FromRoute] Guid authorId)
        {
            var response = await Mediator.Send(new GetAllBooksBySpecificAuthorQueryModel(authorId));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetBooksByCategoryId)]
        public async Task<IActionResult> GetBooksByCategoryId([FromRoute] Guid categoryId)
        {
            var response = await Mediator.Send(new GetAllBooksInASpecificCategoryQueryModel(categoryId));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetBooksOrderedByStockDESC)]
        public async Task<IActionResult> GetBooksOrderedByStockDESC()
        {
            var response = await Mediator.Send(new GetBooksPagintedOrderedByStockDescendingQueryModel());
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetBooksOrderedByStockASC)]
        public async Task<IActionResult> GetBooksOrderedByStockASC()
        {
            var response = await Mediator.Send(new GetBooksPagintedOrderedByStockAscendingQueryModel());
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet(Router.Books.GetAuthorsOfBook)]
        public async Task<IActionResult> GetAuthorsOfBook([FromRoute] Guid Id)
        {
            var response = await Mediator.Send(new GetAllAuthorsAssociatedWithABookQueryModel(Id));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet(Router.Books.GetOutOfStockBooks)]
        public async Task<IActionResult> GetOutOfStockBooks()
        {
            var response = await Mediator.Send(new GetAllOutOfStockBooksQueryModel());
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetBooksByAuthorIdOrderedByViewCount)]
        public async Task<IActionResult> GetBooksByAuthorIdOrderedByViewCount([FromRoute] Guid authorId)
        {
            var response = await Mediator.Send(new GetsBooksByAnAuthorOorderedByViewCountQueryModel(authorId));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetBooksByBothAuthorAndCategory)]
        public async Task<IActionResult> GetBooksByBothAuthorAndCategory([FromRoute] Guid authorId, [FromRoute] Guid categoryId)
        {
            var response = await Mediator.Send(new GetBooksByBothAuthorAndCategoryQueyModel(authorId, categoryId));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Books.GetLowStockBooks)]
        public async Task<IActionResult> GetLowStockBooks([FromRoute] int threshold)
        {
            var response = await Mediator.Send(new GetBooksWithStockBelowAThresholdQueryModel(threshold));
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }



        [Authorize(Roles = "Owner, Admin")]
        [HttpPost(Router.Books.CreateBook)]
        public async Task<IActionResult> CreateBook([FromForm] CreateBookCommandModel model)
        {
            var response = await Mediator.Send(model);
            return FinalResponse(response);
        }
        [HttpPut(Router.Books.UpdateBook)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> UpdateBook([FromForm] UpdateBookCommandModel model)
        {
            var response = await Mediator.Send(model);
            return FinalResponse(response);
        }
        [HttpPut(Router.Books.IncreaseBookStock)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> IncreaseBookStock([FromBody] IncreaseBookStockCommandModel model)
        {
            var response = await Mediator.Send(model);
            return FinalResponse(response);
        }
        [HttpPut(Router.Books.DecreaseBookStock)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> DecreaseBookStock([FromBody] DecreaseBookStockCommandModel model)
        {
            var response = await Mediator.Send(model);
            return FinalResponse(response);
        }
        [HttpDelete(Router.Books.DeleteBook)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid Id)
        {
            var response = await Mediator.Send(new DeleteBookCommandModel(Id));
            return FinalResponse(response);
        }
    }
}
