using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;

namespace ThothShop.Service.Contract
{
    public interface IReviewService
    {

        public Task<string> CreateReviewAsync(Review review);


        public Task<bool> DeleteReviewAsync(Guid id);


        public Task<double> GetAverageRatingForBookAsync(Guid bookId);


        public Task<Review?> GetReviewByIdAsync(Guid id, bool tracked = false);

        public Task<IEnumerable<Review>> GetReviewsByUserAsync(string userId);


        public Task<IEnumerable<Review>> GetReviewsForBookAsync(Guid bookId);


        public Task<bool> UpdateReviewAsync(Review review);
    }
}
           
