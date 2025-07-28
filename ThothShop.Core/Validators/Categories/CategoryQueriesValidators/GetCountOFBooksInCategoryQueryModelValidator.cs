using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Categories.Queries.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Validators.Categories.CategoryQueriesValidators
{
    public class GetCountOFBooksInCategoryQueryModelValidator : AbstractValidator<GetCountOFBooksInCategoryQueryModel>
    {
        #region Field(s)
        private readonly ICategoryService _categoryService;
        #endregion
        #region Constructor(s)
        public GetCountOFBooksInCategoryQueryModelValidator(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.")
                .MustAsync(async (Key, CancellationToken) => await _categoryService.GetCategoryByIdAsync(Key) is not null)
                .WithMessage("Category Dosen't Exists!");
        }
        #endregion
    }
}
