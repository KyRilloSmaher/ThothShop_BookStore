using FluentValidation;
using ThothShop.Core.Features.WishLists.Commands.Models;

namespace ThothShop.Core.Validators.WishLists.CommandsModelsValidator
{
    public class ClearWishlistCommandValidator : AbstractValidator<ClearWishlistCommnandModel>
    {
        public ClearWishlistCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
}