using FluentValidation;
using ThothShop.Core.Features.Reviews.Commands.Models;

namespace ThothShop.Core.Validators.Reviews.CommandModelValidators
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommandModel>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Review ID is required");

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