using FluentValidation;
using ThothShop.Core.Features.WishLists.Commands.Models;

namespace ThothShop.Core.Validators.WishLists.CommandsModelsValidator
{
    public class AddToWishListCommandValidator : AbstractValidator<AddToWishListCommandModel>
    {
        public AddToWishListCommandValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty()
                .WithMessage("Book ID is required");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
}