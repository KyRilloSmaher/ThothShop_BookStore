using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.RepositoriesContracts
{
    public interface IAuthorCategoriesRepository :IGenericRepository<AuthorCategories>
    {
        Task<bool> AddAuthorToCategoriesAsync(AuthorCategories authorCategories);
        Task<bool> RemoveAuthorFromCategoriesAsync(AuthorCategories authorCategories);
        Task<bool> IsAuthorInCategoryAsync(Guid authorId, Guid categoryId);
        Task<IEnumerable<Guid>> GetCategoriesByAuthorIdAsync(Guid authorId);
        Task<IEnumerable<Guid>> GetAuthorsByCategoryIdAsync(Guid categoryId);
    }
}
