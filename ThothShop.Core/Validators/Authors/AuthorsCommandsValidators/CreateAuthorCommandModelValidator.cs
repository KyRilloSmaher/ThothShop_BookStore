using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Authors.Commands.Models;

namespace ThothShop.Core.Validators.Authors.AuthorsCommandsValidators
{
    public class CreateAuthorCommandModelValidator : AbstractValidator<CreateAuthorCommandModel>
    {
        public CreateAuthorCommandModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.Bio)
                .NotEmpty().WithMessage("Bio is required")
                .MaximumLength(1000).WithMessage("Bio must not exceed 1000 characters");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender is required");

            RuleFor(x => x.Nationality)
                .IsInEnum().WithMessage("Nationality is required");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required")
                .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of Birth must be in the past");
        }
    }
}
