using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.Contracts;

namespace ThothShop.Infrastructure.Implementions
{
    public class ReviewRepository : GenericRepository<Review> , IReviewRepository
    {
        private readonly ApplicationDBContext _context;
        public ReviewRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<double> GetAverageRatingForBookAsync(Guid bookId)
        {
           return await _context.Reviews.Where(r => r.BookId == bookId).AverageAsync(r => r.Rating);
        }

        public async Task<Review?> GetReviewByIdAsync(Guid reviewId, bool tracked = false)
        {
           return tracked
                ? await _context.Reviews.Include(r => r.User).Include(r=>r.Book).ThenInclude(b=>b.BookImages).FirstOrDefaultAsync(r => r.Id == reviewId)
                : await _context.Reviews.AsNoTracking().Include(r => r.User).Include(r => r.Book).ThenInclude(b => b.BookImages).FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        //public async Task<IEnumerable<(int Rating, int Count)>> GetRatingDistributionAsync(Guid bookId)
        //{
        //    return await _context.Reviews
        //                  .Where(r => r.BookId == bookId)
        //                  .GroupBy(r => r.Rating)
        //                  .Select(g => (g.Key, g.Count())) // Use tuple instead of an anonymous type
        //                  .OrderBy(g => g.Key)
        //                  .ToListAsync();
        //}

        public async Task<int> GetReviewCountForBookAsync(Guid bookId)
        {
            return await _context.Reviews.CountAsync(r => r.BookId == bookId);
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookAsync(Guid bookId, int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Reviews
            .Where(r => r.BookId == bookId)
            .Include(r => r.User)
            .Include(r=>r.Book)
            .ThenInclude(b=>b.BookImages)
            .OrderByDescending(r => r.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserAsync(string userId, int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Reviews.Include(r => r.User).Where(o => o.UserId == userId).Include(b=>b.Book)
                .OrderByDescending(r => r.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<bool> HasUserReviewedBookAsync(string userId, Guid bookId)
        {
            return await _context.Reviews
             .AnyAsync(r => r.UserId == userId && r.BookId == bookId);
        }
    }
}
