using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Book>> GetPopularBooksByCategoryAsync(Guid categoryId, int count = 5);
        Task<bool> CategoryExistsByNameAsync(string name);
        Task<IEnumerable<Category>> GetPopularCategoriesAsync();
        Task<int> GetBooksCountInCategoryAsync(Guid categoryId);

        Task<Category?> GetCategoryByNameAsync(string categoryName);
    }
}
