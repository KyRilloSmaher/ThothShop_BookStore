using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.RepositoriesContracts;

namespace ThothShop.Infrastructure.RepositoriesImplementions
{
    public class AuthorCategoriesRepository: GenericRepository<AuthorCategories>,IAuthorCategoriesRepository
    {
        private readonly ApplicationDBContext _context;

        public AuthorCategoriesRepository(ApplicationDBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<bool> AddAuthorToCategoriesAsync(AuthorCategories authorCategories)
        {
            var result =await AddAsync(authorCategories);
            return result is not null;
        }

        public async Task<IEnumerable<Guid>> GetAuthorsByCategoryIdAsync(Guid categoryId)
        {
           return await _context.AuthorCategories
                        .Where(ac => ac.CategoryId == categoryId)
                        .Select(ac => ac.AuthorId)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetCategoriesByAuthorIdAsync(Guid authorId)
        {
           return await _context.AuthorCategories
                        .Where(ac => ac.AuthorId == authorId)
                        .Select(ac => ac.CategoryId)
                        .ToListAsync();
        }

        public async Task<bool> IsAuthorInCategoryAsync(Guid authorId, Guid categoryId)
        {
          return  await _context.AuthorCategories.FirstOrDefaultAsync(ac=>ac.AuthorId == authorId && ac.CategoryId == categoryId) != null;
        }

        public async Task<bool> RemoveAuthorFromCategoriesAsync(AuthorCategories authorCategories)
        {
             await DeleteAsync(authorCategories);
            return true;
        }
    }
}
