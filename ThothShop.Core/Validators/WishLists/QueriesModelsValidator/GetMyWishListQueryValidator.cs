using FluentValidation;
using ThothShop.Core.Features.WishLists.Queries.Models;

namespace ThothShop.Core.Validators.WishLists.QueriesModelsValidator
{
    public class GetMyWishListQueryValidator : AbstractValidator<GetMyWishListQueryModel>
    {
        public GetMyWishListQueryValidator()
        {
           /*RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");*/
        }
    }
}