using FluentValidation;
using ThothShop.Core.Features.Reviews.Commands.Models;

namespace ThothShop.Core.Validators.Reviews.CommandModelValidators
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommandModel>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty()
                .WithMessage("Book ID is required");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");

            RuleFor(x => x.Rating)
                .NotEmpty()
                .WithMessage("Rating is required")
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5");

            RuleFor(x => x.Comment)
                .NotEmpty()
                .WithMessage("Comment is required")
                .MaximumLength(1000)
                .WithMessage("Comment cannot exceed 1000 characters");
        }
    }
}