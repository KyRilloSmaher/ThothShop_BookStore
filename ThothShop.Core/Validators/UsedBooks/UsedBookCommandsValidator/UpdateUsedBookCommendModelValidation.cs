using FluentValidation;
using Microsoft.AspNetCore.Http;
using ThothShop.Core.Features.UsedBooks.Commands.Models;

namespace ThothShop.Core.Validators.UsedBooks.UsedBookCommandsValidator
{

    public class UpdateUsedBookCommandModelValidator : AbstractValidator<UpdateUsedBookCommandModel>
    {
        public UpdateUsedBookCommandModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Used book ID is required.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.PublishedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Published date cannot be in the future.");

            RuleFor(x => x.PrimaryImage)
                .Must(BeAValidImage)
                .When(x => x.PrimaryImage != null)
                .WithMessage("Primary image must be a valid image file (jpg, jpeg, png, webp).");

            RuleForEach(x => x.Images)
                .Must(BeAValidImage)
                .When(x => x.Images != null && x.Images.Any())
                .WithMessage("Each image must be a valid image file (jpg, jpeg, png, webp).");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User Id is required.");

            RuleFor(x => x.Condition)
                .IsInEnum().WithMessage("Invalid book condition value.");

            RuleFor(x => x.Note_WayOfConnect)
                .NotEmpty().WithMessage("Way of contact is required.");
        }

        private bool BeAValidImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }
    }

}
