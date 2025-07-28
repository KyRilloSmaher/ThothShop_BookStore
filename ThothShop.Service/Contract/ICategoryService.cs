using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Service.Contract
{
    public interface ICategoryService
    {
        Task<Category?> GetCategoryByIdAsync(Guid id , bool asTracked = false);
        Task<Category?> GetCategoryByNameAsync(string name);
        Task<Category?> GetCategoryAsTrackedByIdAsync(Guid id);
        Task<int> GetNumberOfBooksInCategory(Guid Id);
        Task<bool> IsCategoryNameExistAsync(string name);
        Task<bool> IsCategoryNameExistExceptitselfAsync(Guid Id ,string name);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetPopularCategoriesAsync();
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId);
        Task<Category?> CreateCategoryAsync(Category category , IFormFile Icon);
        Task<bool> UpdateCategoryAsync( Category category,IFormFile Icon);
        Task<bool> DeleteCategoryAsync(Guid id);
        Task<bool> AddAuthorToCategoriesAsync(Guid authorId, List<Guid> categoryIds);
    }
}
