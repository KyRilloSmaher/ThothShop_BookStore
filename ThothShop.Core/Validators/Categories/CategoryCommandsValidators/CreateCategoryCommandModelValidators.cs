using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Categories.Commands.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Validators.Categories.CategoryCommandsValidators
{
    public class CreateCategoryCommandModelValidators:AbstractValidator<CreateCategoryCommandModel>
    {
        #region Field(s)
        private readonly ICategoryService _categoryService;
        #endregion

        #region Constructor(s)
        public CreateCategoryCommandModelValidators(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

        
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Category name is required.")
                    .MaximumLength(100)
                    .WithMessage("Category name must not exceed 100 characters.")
                    .MustAsync(async (Key, CancellationToken) => !await _categoryService.IsCategoryNameExistAsync(Key))
                    .WithMessage("This Category name is already Exist !");
         
        }
        #endregion

 

    }
}
