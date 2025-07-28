using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.Authors.Queries.Models;
using ThothShop.Core.Features.Categories.Commands.Models;
using ThothShop.Core.Features.Categories.Queries.Models;
using ThothShop.Domain.AppMetaData;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class CategoryController : ApplicationBaseController
    {
        public CategoryController() { }


        [HttpGet(Router.Categories.GetById)]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid Id)
        {
            var Query = new GetCategoryByIdQueryModel(Id);
            var response = await Mediator.Send(Query);
            return FinalResponse(response);
        }

        [HttpGet(Router.Categories.GetByName)]
        public async Task<IActionResult> GetCategoriesByName([FromRoute] string name)
        {
            var Query = new GetCategoryBynameQueryModel(name);
            var response = await Mediator.Send(Query);
            return FinalResponse(response);
        }
        
        [HttpGet(Router.Categories.GetAll)]
        public async Task<IActionResult> GetAllCategories()
        {
            var Query = new GetAllCategoriesQueryModel();
            var response = await Mediator.Send(Query);
            return FinalResponse(response);
        }

        [HttpGet(Router.Categories.GetPopularCategories)]
        public async Task<IActionResult> GetPopularCategories()
        {
            var Query = new GetPopularCategoriesQueryModel();
            var response = await Mediator.Send(Query);
            return FinalResponse(response);
        }
        [HttpGet(Router.Categories.GetBooksCountInCategory)]
        public async Task<IActionResult> GetCountOFBooksInCategory([FromRoute] Guid Id)
        {
            var Query = new GetCountOFBooksInCategoryQueryModel(Id);
            var response = await Mediator.Send(Query);
            return FinalResponse(response);
        }

        [HttpPost(Router.Categories.CreateCategory)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryCommandModel command)
        {
            var response = await Mediator.Send(command);
            return FinalResponse(response);
        }
        [HttpPost(Router.Categories.AddAuthorToCategories)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> AddAuthorToCategories([FromForm] AddAuthorToCategoriesCommandModel command)
        {
            var response = await Mediator.Send(command);
            return FinalResponse(response);
        }
        [HttpPut(Router.Categories.UpdateCategory)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryCommandModel command)
        {
            var response = await Mediator.Send(command);
            return FinalResponse(response);
        }
        [HttpDelete(Router.Categories.DeleteCategory)]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> DeleteCategory([FromRoute]Guid Id)
        {
            var command = new DeleteCategoryCommandModel(Id);
            var response = await Mediator.Send(command);
            return FinalResponse(response);
        }

    }
}
