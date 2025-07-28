using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.Contracts
{
    public interface IReviewRepository :IGenericRepository<Review>
    {
        Task<Review?> GetReviewByIdAsync(Guid reviewId,bool tracked = false);
        Task<IEnumerable<Review>> GetReviewsByBookAsync(Guid bookId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<Review>> GetReviewsByUserAsync(string userId, int pageNumber = 1, int pageSize = 10);
        Task<double> GetAverageRatingForBookAsync(Guid bookId);
        Task<int> GetReviewCountForBookAsync(Guid bookId);
        Task<bool> HasUserReviewedBookAsync(string userId, Guid bookId);

        //Task<IEnumerable<(int Rating, int Count)>> GetRatingDistributionAsync(Guid bookId);
    }
}
