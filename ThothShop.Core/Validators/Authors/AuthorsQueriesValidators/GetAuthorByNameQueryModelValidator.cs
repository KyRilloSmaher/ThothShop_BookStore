using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Authors.Queries.Models;

namespace ThothShop.Core.Validators.Authors.AuthorsQueriesValidators
{
    public class GetAuthorByNameQueryModelValidator:AbstractValidator<GetAuthorsFilteredQueryModel>
    {
        public GetAuthorByNameQueryModelValidator()
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(100)
                .WithMessage("Author name must not exceed 100 characters.");
            RuleFor(x => x.Nationality)
                   .IsInEnum()
                   .WithMessage("Invalid nationality.");
        }
    }
}
