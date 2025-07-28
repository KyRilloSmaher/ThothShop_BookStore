using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Service.Contract;
using ThothShop.Domain.Helpers;

namespace ThothShop.Service.implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        }

        public async Task<string> CreateReviewAsync(Review review)
        {

            await _reviewRepository.AddAsync(review);

            return string.Format(SystemMessages.CreatedSuccessfully,"Review");
        }

        public async Task<bool> DeleteReviewAsync(Guid id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id,true);
            if (review == null)
            {
                return false;
            }
            await _reviewRepository.DeleteAsync(review);

            return true;
        }

        public async Task<double> GetAverageRatingForBookAsync(Guid bookId)
        {
          return await _reviewRepository.GetAverageRatingForBookAsync(bookId);
        }

        public async Task<Review?> GetReviewByIdAsync(Guid id , bool tracked =false)
        {
            return await _reviewRepository.GetReviewByIdAsync(id, tracked);
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserAsync(string userId)
        {
          return await _reviewRepository.GetReviewsByUserAsync(userId);
        }

        public async Task<IEnumerable<Review>> GetReviewsForBookAsync(Guid bookId)
        {
           return await _reviewRepository.GetReviewsByBookAsync(bookId);
        }

        public async Task<bool> UpdateReviewAsync(Review review)
        {

            await _reviewRepository.UpdateAsync(review);
            await _reviewRepository.SaveChangesAsync();

            return true;
        }
    }
}