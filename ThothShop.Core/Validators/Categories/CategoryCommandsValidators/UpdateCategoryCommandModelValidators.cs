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
    public class UpdateCategoryCommandModelValidators: AbstractValidator<UpdateCategoryCommandModel>
    {
        #region Field(s)
        private readonly ICategoryService _categoryService;
        #endregion

        #region Constructor(s)
        public UpdateCategoryCommandModelValidators(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Category ID is required.")
                    .MustAsync(async (id, CancellationToken) => await _categoryService.GetCategoryByIdAsync(id) != null)
                    .WithMessage("Category not found.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name is required.")
                .MaximumLength(100)
                .WithMessage("Category name must not exceed 100 characters.")
                .MustAsync(async (model, Key, CancellationToken) => !await _categoryService.IsCategoryNameExistExceptitselfAsync(model.Id, model.Name))
                .WithMessage("This Category name is already Used!");

        }
        #endregion

      
    }
}
