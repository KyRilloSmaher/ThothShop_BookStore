using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Categories.Queries.Models;

namespace ThothShop.Core.Validators.Categories.CategoryQueriesValidators
{
    public class GetCategoryByIdQueryValidator:AbstractValidator<GetCategoryByIdQueryModel>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Category Id is required.");
        }
    }
}
