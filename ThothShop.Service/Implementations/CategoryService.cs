
using ThothShop.Service.Contract;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Infrastructure.RepositoriesContracts;
using Microsoft.AspNetCore.Http;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Helpers;
using Microsoft.Extensions.Logging;
using ThothShop.Service.Implementations;

namespace ThothShop.Service.implementations
{
    public class CategoryService : ICategoryService
    {

        private readonly IAuthorCategoriesRepository _authorCategoriesRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuhtorService _authorService;
        private readonly IImageUploaderService _imageUploaderService;
        private readonly IImageRepository _imageRepository;
        private readonly ILogger<BookService> _logger;
        public CategoryService(IAuthorCategoriesRepository authorCategoriesRepository, ICategoryRepository categoryRepository, IAuhtorService authorService, IImageUploaderService imageUploaderService, ILogger<BookService> logger, IImageRepository imageRepository)
        {
            _authorCategoriesRepository = authorCategoriesRepository;
            _categoryRepository = categoryRepository;
            _authorService = authorService;
            _imageUploaderService = imageUploaderService;
            _logger = logger;
            _imageRepository = imageRepository;
        }

        public async Task<Category?> CreateCategoryAsync(Category category,IFormFile Icon)
        {

            try
            {
                // Begin a transaction.
                await _categoryRepository.BeginTransactionAsync();

                // Add the new book.
                var addedCategory = await _categoryRepository.AddAsync(category);
                if (addedCategory is null)
                {
                    throw new Exception(SystemMessages.FailedToAddEntity);
                }

                // Process primary image.
                var primaryResult = await _imageUploaderService.UploadImageAsync(Icon, ImageFolder.CategoryImage);
                var primaryImageUrl = primaryResult.Url.ToString();
                var primaryImageEntity = new Image
                {
                    Url = primaryImageUrl,
                    IsPrimary = true
                };

                await _imageRepository.AddAsync(primaryImageEntity);

                await _categoryRepository.CommitAsync();
                return addedCategory;
            }
            catch (Exception ex)
            {
                await _categoryRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to create Category with ID {addedCategory.Id}", category.Id);
                throw new Exception(SystemMessages.FailedToAddEntity, ex);
            }
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id ,bool asTracked = false)
        {
            return await _categoryRepository.GetByIdAsync(id, asTracked);
        
        }
        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _categoryRepository.GetCategoryByNameAsync(name);
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<IEnumerable<Category>> GetPopularCategoriesAsync()
        {
            return await _categoryRepository.GetPopularCategoriesAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid categoryId)
        {
            var categoryExists = await _categoryRepository.GetByIdAsync(categoryId) != null;
            if (!categoryExists)
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

            return await _categoryRepository.GetBooksByCategoryAsync(categoryId);
        }

        public async Task<bool> UpdateCategoryAsync(Category category, IFormFile? Icon)
        {
            try
            {
                await _categoryRepository.BeginTransactionAsync();
                var categoryId = category.Id;

                // Update the book details.
                await _categoryRepository.UpdateAsync(category);


                if (Icon != null)
                {
                    var existingPrimary = await _imageRepository.GetByIdAsync(categoryId);
                    if (existingPrimary != null)
                    {
                        await _imageUploaderService.DeleteImageByUrlAsync(existingPrimary.Url);
                        await _imageRepository.DeleteAsync(existingPrimary);
                    }

                    var primaryResult = await _imageUploaderService.UploadImageAsync(Icon, ImageFolder.BookPrimaryImage);
                    var primaryImageUrl = primaryResult.Url.ToString();
                    var primaryImageEntity = new Image
                    {
                        Url = primaryImageUrl,
                        IsPrimary = true
                    };
                    await _imageRepository.AddAsync(primaryImageEntity);
                }
               

                await _categoryRepository.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _categoryRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to update category with ID {category.Id}", category.Id);
                throw new Exception(SystemMessages.FailedToUpdateEntity, ex);
            }
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            try
            {
                await _categoryRepository.BeginTransactionAsync();
                var category = await _categoryRepository.GetByIdAsync(id);
                // Check if category has any books before deleting
                var booksCount = await _categoryRepository.GetBooksCountInCategoryAsync(id);
                if (booksCount > 0)
                    throw new InvalidOperationException("Cannot delete category that has associated books.");
                var Image = category.Icon;
                await _imageUploaderService.DeleteImageByUrlAsync(Image.Url);
                await _categoryRepository.DeleteAsync(category);
                await _categoryRepository.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _categoryRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to delete Category with ID {id}", id);
                throw new Exception(SystemMessages.FailedToAddEntity, ex);
            }
        }

        public async Task<bool> IsCategoryNameExistAsync(string name)
        {
          return await _categoryRepository.CategoryExistsByNameAsync(name);
        }
        public async Task<Category?> GetCategoryAsTrackedByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
        public async Task<int> GetNumberOfBooksInCategory(Guid Id)
        {
            return await _categoryRepository.GetBooksCountInCategoryAsync(Id);
        }
        public async Task<bool> IsCategoryNameExistExceptitselfAsync(Guid Id, string name)
        {
            var categoryNameExists = await _categoryRepository.GetCategoryByNameAsync(name);
            if (categoryNameExists is not null)
            {
                return categoryNameExists.Id != Id;
            }
            return false;
        }

        public async Task<bool> AddAuthorToCategoriesAsync(Guid authorId, List<Guid> categoryIds)
        {
            var IsauthorExist = await _authorService.IsAuthorExist(authorId);
            if (!IsauthorExist)
                throw new KeyNotFoundException($"Author with ID {authorId} not found.");

            foreach (var categoryId in categoryIds)
            {
                var IscategoryExist = await GetCategoryByIdAsync(categoryId) != null;
                if (IscategoryExist)
                {
                    var isAuthorInCategory = await _authorCategoriesRepository.IsAuthorInCategoryAsync(authorId, categoryId);
                    if (isAuthorInCategory)
                    {
                        // Author is already in this category, skip adding
                        continue;
                    }
                    // Author is not in this category, add them
                    else
                    {                        
                        var newObject = new AuthorCategories
                        {
                            AuthorId = authorId,
                            CategoryId = categoryId
                        };
                        await _authorCategoriesRepository.AddAsync(newObject);
                    }
                }
            }
            return true;

        }
    }
}
