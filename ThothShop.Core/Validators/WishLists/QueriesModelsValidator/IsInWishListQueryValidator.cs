using FluentValidation;
using ThothShop.Core.Features.WishLists.Queries.Models;

namespace ThothShop.Core.Validators.WishLists.QueriesModelsValidator
{
    public class IsInWishListQueryValidator : AbstractValidator<IsInWishListQueryModel>
    {
        public IsInWishListQueryValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty()
                .WithMessage("Book ID is required")
                .Must(id => id != Guid.Empty)
                .WithMessage("Invalid Book ID");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required")
                .MinimumLength(1)
                .WithMessage("User ID cannot be empty")
                .MaximumLength(450) 
                .WithMessage("User ID exceeds maximum length");
        }
    }
}