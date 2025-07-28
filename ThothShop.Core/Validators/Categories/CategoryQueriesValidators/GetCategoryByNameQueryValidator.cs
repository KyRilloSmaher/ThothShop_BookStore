using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Categories.Queries.Models;

namespace ThothShop.Core.Validators.Categories.CategoryQueriesValidators
{
    public class GetCategoryByNameQueryValidator:AbstractValidator<GetCategoryBynameQueryModel>
    {
        public GetCategoryByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty().WithMessage("Category Name is required.");

        }
    }
}
