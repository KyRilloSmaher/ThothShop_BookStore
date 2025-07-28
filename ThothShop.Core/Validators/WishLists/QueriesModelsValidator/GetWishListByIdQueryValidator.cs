using FluentValidation;
using ThothShop.Core.Features.WishLists.Queries.Models;

namespace ThothShop.Core.Validators.WishLists.QueriesModelsValidator
{
    public class GetWishListByIdQueryValidator : AbstractValidator<GetWishListByIdQueryModel>
    {
        public GetWishListByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Wishlist ID is required")
                .Must(id => id != Guid.Empty)
                .WithMessage("Invalid Wishlist ID");
        }
    }
}