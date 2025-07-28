using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ThothShop.Core.Features.Categories.Commands.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Validators.Categories.CategoryCommandsValidators
{
    public class AddAuthorToCategoriesQueryModelValidators : AbstractValidator<AddAuthorToCategoriesCommandModel>
    {
        #region Field(s)
        private readonly ICategoryService _categoryService;
        private readonly IAuhtorService _authorService;
        #endregion
        public AddAuthorToCategoriesQueryModelValidators(ICategoryService categoryService, IAuhtorService authorService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .WithMessage("Author Id is required.")
                 .MustAsync(async (Key, CancellationToken) => 
                 
                 await _authorService.IsAuthorExist(Key))
                .WithMessage("Author Dosen't Exists!");
            RuleFor(x => x.CategoryIds)
                .NotNull()
                .WithMessage("Category Ids can not be Null.")
                .NotEmpty()
                .WithMessage("Category Ids are required.")
                .MustAsync(async (model, Key, CancellationToken) =>
                {
                    foreach (var item in model.CategoryIds)
                    {
                        var category = await _categoryService.GetCategoryByIdAsync(item);
                        if (category is null)
                        {
                            return false;
                        }
                    }
                    return true;
                });
           
        }
    }
}
