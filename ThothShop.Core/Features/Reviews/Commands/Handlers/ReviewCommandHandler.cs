using AutoMapper;
using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Reviews.Commands.Models;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Reviews.Commands.Handlers
{
    public class ReviewCommandHandler : ResponseHandler,
        IRequestHandler<CreateReviewCommandModel, Response<string>>,
        IRequestHandler<UpdateReviewCommandModel, Response<string>>,
        IRequestHandler<DeleteReviewCommandModel, Response<string>>
    {
        #region Field(s)
        private readonly IReviewService _reviewService;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        #endregion
        public ReviewCommandHandler(IReviewService reviewService, IMapper mapper, IBookRepository bookRepository)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<Response<string>> Handle(CreateReviewCommandModel request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
               throw new NullReferenceException("CreateReviewCommandModel Can not be null");
            }
            var existingBook = await _bookRepository.GetByIdAsync(request.BookId);
            if (existingBook is null)
            {
                return BadRequest<string>("book Can not be Found !");
            }
            var review = _mapper.Map<Review>(request);
            var result = await _reviewService.CreateReviewAsync(review);
            if (string.IsNullOrEmpty(result))
                return BadRequest<string>("Failed to create review");

            return Success(result);
        }

        public async Task<Response<string>> Handle(UpdateReviewCommandModel request, CancellationToken cancellationToken)
        {
            var existingReview = await _reviewService.GetReviewByIdAsync(request.Id,true);
            if (existingReview == null)
                return NotFound<string>("Review not found");

            _mapper.Map(request, existingReview);

            var result = await _reviewService.UpdateReviewAsync(existingReview);
            if (!result)
                return BadRequest<string>("Failed to update review");

            return Success("Review updated successfully");
        }

        public async Task<Response<string>> Handle(DeleteReviewCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _reviewService.DeleteReviewAsync(request.Id);
            if (!result)
                return NotFound<string>("Review not found or could not be deleted");

            return Success("Review deleted successfully");
        }
    }
} 