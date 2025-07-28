using AutoMapper;
using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Reviews.Queries.Models;
using ThothShop.Core.Features.Reviews.Queries.Responses;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Reviews.Queries.Handlers
{
    public class ReviewQueryHandler : ResponseHandler,
        IRequestHandler<GetReviewByIdQueryModel, Response<ReviewResponse>>,
        IRequestHandler<GetReviewsByUserQueryModel, Response<IEnumerable<ReviewResponse>>>,
        IRequestHandler<GetReviewsByBookQueryModel, Response<IEnumerable<ReviewResponse>>>,
        IRequestHandler<GetBookAverageRatingQueryModel, Response<double>>
    {
        #region Private Field(s)
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public ReviewQueryHandler(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }
        #endregion


        #region Method(s)

        public async Task<Response<ReviewResponse>> Handle(GetReviewByIdQueryModel request, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetReviewByIdAsync(request.Id);
            var reviewResponse = _mapper.Map<ReviewResponse>(review);
            if (review == null)
                return NotFound<ReviewResponse>("Review not found");

            return Success(reviewResponse);
        }

        public async Task<Response<IEnumerable<ReviewResponse>>> Handle(GetReviewsByUserQueryModel request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetReviewsByUserAsync(request.UserId);
            if (reviews == null)
                return NotFound<IEnumerable<ReviewResponse>>("No reviews found for this user");
            var reviewResponses = _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
            return Success(reviewResponses);
        }

        public async Task<Response<IEnumerable<ReviewResponse>>> Handle(GetReviewsByBookQueryModel request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetReviewsForBookAsync(request.BookId);
            if (reviews == null)
                return NotFound<IEnumerable<ReviewResponse>>("No reviews found for this book");
            var reviewResponses = _mapper.Map<IEnumerable<ReviewResponse>>(reviews);

            return Success(reviewResponses);
        }

        public async Task<Response<double>> Handle(GetBookAverageRatingQueryModel request, CancellationToken cancellationToken)
        {
            var averageRating = await _reviewService.GetAverageRatingForBookAsync(request.BookId);
            return Success(averageRating);
        }
        #endregion
    }
} 