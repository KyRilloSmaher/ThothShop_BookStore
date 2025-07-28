using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.Reviews.Commands.Models;
using ThothShop.Core.Features.Reviews.Queries.Models;
using ThothShop.Core.Reviews.DTOS;
using ThothShop.Domain.AppMetaData;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ReviewController : ApplicationBaseController
    {
        public ReviewController() { }

        [HttpGet(Router.Reviews.GetById)]
        public async Task<IActionResult> GetReviewById([FromRoute] Guid Id)
        {
            var response = await Mediator.Send(new GetReviewByIdQueryModel(Id));
            return FinalResponse(response);
        }

        [HttpGet(Router.Reviews.GetByUser)]
        public async Task<IActionResult> GetReviewsByUser()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = await Mediator.Send(new GetReviewsByUserQueryModel(userId));
            return FinalResponse(response);
        }

        [HttpGet(Router.Reviews.GetByBook)]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewsByBook([FromRoute] Guid bookId)
        {
            var response = await Mediator.Send(new GetReviewsByBookQueryModel(bookId));
            return FinalResponse(response);
        }

        [HttpGet(Router.Reviews.GetAverageRating)]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookAverageRating([FromRoute] Guid bookId)
        {
            var response = await Mediator.Send(new GetBookAverageRatingQueryModel(bookId));
            return FinalResponse(response);
        }

        [HttpPost(Router.Reviews.CreateReview)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO dto)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = new CreateReviewCommandModel { 
               UserId = UserId,
               BookId = dto.bookId,
               Comment = dto.Comment,
               Rating = dto.rating

            };
            var response = await Mediator.Send(model);
            return FinalResponse(response);
        }

        [HttpPut(Router.Reviews.UpdateReview)]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewCommandModel model)
        {
            var response = await Mediator.Send(model);
            return FinalResponse(response);
        }

        [HttpDelete(Router.Reviews.DeleteReview)]
        public async Task<IActionResult> DeleteReview([FromRoute] Guid Id)
        {
            var response = await Mediator.Send(new DeleteReviewCommandModel(Id));
            return FinalResponse(response);
        }
    }
} 