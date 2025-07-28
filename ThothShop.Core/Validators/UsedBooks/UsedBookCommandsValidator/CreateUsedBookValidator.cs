using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.UsedBooks.Commands.Models;

namespace ThothShop.Core.Validators.UsedBooks.UsedBookCommandsValidator
{

    public class CreateUsedBookCommandModelValidator : AbstractValidator<CreateUsedBookCommandModel>
    {
        public CreateUsedBookCommandModelValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title must be 150 characters or fewer.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Price)
                .Must(p=>p>=0).WithMessage("Price must be greater than or Equal 0.");

            RuleFor(x => x.PublishedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Published date cannot be in the future.");

            RuleFor(x => x.PrimaryImage)
                .NotNull().WithMessage("Primary image is required.")
                .Must(BeAValidImage).WithMessage("Primary image must be a valid image file (jpg, jpeg, png, webp).");

            RuleForEach(x => x.Images)
                .Must(BeAValidImage).WithMessage("Each image must be a valid image file (jpg, jpeg, png, webp).")
                .When(x => x.Images != null && x.Images.Any());

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User Id is required.");

            RuleFor(x => x.Condition)
                .IsInEnum().WithMessage("Invalid condition value.");

            RuleFor(x => x.Note_WayOfConnect)
                .NotEmpty().WithMessage("Way of contact note is required.");
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
