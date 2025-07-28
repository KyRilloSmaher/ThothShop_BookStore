using FuzzySharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.Contracts;

namespace ThothShop.Infrastructure.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId)
        {
            return await _context.Books
                .Where(b => b.CategoryId == categoryId)
                .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetPopularBooksByCategoryAsync(Guid categoryId, int count = 10)
        {
            return await _context.Books
                .Where(b => b.CategoryId == categoryId)
                .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                .OrderByDescending(b => b.ViewCount)
                .ThenByDescending(b => b.Reviews.Average(r => r.Rating))
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            return await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Category>> GetPopularCategoriesAsync()
        {
            return await _context.Categories
                .OrderByDescending(c => c.Books.Sum(b => b.ViewCount)).Include(c => c.Icon).AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetBooksCountInCategoryAsync(Guid categoryId)
        {
            return await _context.Books
                .CountAsync(b => b.CategoryId == categoryId);
        }
        public override async Task<Category?> GetByIdAsync(Guid categoryId, bool AsTracked = false)
        {
            return AsTracked ?
                          await _context.Categories.Include(c=>c.Icon).AsTracking().FirstOrDefaultAsync(c => c.Id == categoryId)
                         :await _context.Categories.Include(c => c.Icon).AsNoTracking().FirstOrDefaultAsync(c => c.Id == categoryId);
        }
        public async Task<Category?> GetCategoryByNameAsync(string categoryName)
        {
                var categories = await _context.Categories
                    .AsNoTracking().Include(c => c.Icon)
                    .ToListAsync(); // fetch all categories

                return categories.FirstOrDefault(c => Fuzz.Ratio(c.Name.ToLower(), categoryName.ToLower()) >= 80);

        }

        public override async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories.Include(c => c.Icon).AsNoTracking().ToListAsync();
        }
    }
}