using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.Authors.Commands.Models;
using ThothShop.Core.Features.Authors.Queries.Models;
using ThothShop.Domain.AppMetaData;
using ThothShop.Domain.Enums;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class AuthorController : ApplicationBaseController
    {
        public AuthorController() { }

        [HttpGet(Router.Author.GetById)]
        public async Task<IActionResult> GetAuthorById([FromRoute]Guid Id) { 
          var Query = new GetAuthorByIdQueryModel( Id );
          var response = await Mediator.Send(Query);
            return FinalResponse(response);
        }
        [HttpGet(Router.Author.GetAll)]
        public async Task<IActionResult> GetAllAuthor()
        {
            var Query = new GetAllAuthorsQueryModel();
            var response = await Mediator.Send(Query);
            return FinalResponse(response);
        }
        [HttpGet(Router.Author.TotalAuthors)]
        public async Task<IActionResult> GetAuthorsCount() {

            var Query = new GetAuthorsCountQueryModel();
            var response = await Mediator.Send(Query);
            return FinalResponse(response);
        }

        [HttpGet(Router.Author.GetAllAuthorPaginted)]
        public async Task<IActionResult> GetAllAuthorPaginted([FromQuery] GetAllAuthorsByPaginationQueryModel model)
        {
            var response = await Mediator.Send(model);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        } 
        [HttpGet(Router.Author.FilterAuthors)]
        public async Task<IActionResult> FilterAuthors([FromQuery] GetAuthorsFilteredQueryModel model)
        {
            var response = await Mediator.Send(model);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet(Router.Author.PopularAuthors)]
        public async Task<IActionResult> GetPopularAuthors([FromQuery] GetPopularAuthorsQueryModel model)
        {
            var response = await Mediator.Send(model);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPost(Router.Author.CreateAuthor)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> CreateAuthor([FromForm] CreateAuthorCommandModel model)
        {
            var response = await Mediator.Send(model);
            if (response.Succeeded)
            {
                return FinalResponse(response);
            }
            return BadRequest(response);
        }
        [HttpPut(Router.Author.UpdateAuthor)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> UpdateAuthor([FromForm] UpdateAuthorCommandModel model)
        {
            var response = await Mediator.Send(model);
            if (response.Succeeded)
            {
                return FinalResponse(response);
            }
            return BadRequest(response);
        }
        [HttpDelete(Router.Author.DeleteAuthor)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] Guid Id)
        {
            var command = new DeleteAuthorCommandModel(Id);
            var response = await Mediator.Send(command);
            if (response.Succeeded)
            {
                return FinalResponse(response);
            }
            return BadRequest(response);
        }
    }
}
