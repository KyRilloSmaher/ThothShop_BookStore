using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Infrastructure.Implementations;
using ThothShop.Service.Contract;
using static System.Net.Mime.MediaTypeNames;
using static ThothShop.Domain.AppMetaData.Router;
using Image = ThothShop.Domain.Models.Image;

namespace ThothShop.Service.implementations
{
    public class UsedBookService : IUsedBookService
    {
        private readonly IUsedBookRepository _usedBookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageUploaderService _imageUploaderService;
        private readonly IBookImageService _bookImageService;
        private readonly ILogger<UsedBookService> _logger;

        public UsedBookService(
            IUsedBookRepository usedBookRepository,
            IUserRepository userRepository,
            IImageUploaderService imageUploaderService,
            IBookImageService bookImageService,
            ILogger<UsedBookService> logger)
        {
            _usedBookRepository = usedBookRepository ?? throw new ArgumentNullException(nameof(usedBookRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _imageUploaderService = imageUploaderService;
            _bookImageService = bookImageService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<UsedBook>> GetAllUsedBooksAsync()
        {
            return await _usedBookRepository.GetAll();
        }

        public async Task<string> CreateUsedBookAsync(UsedBook book, IFormFile primaryImage, ICollection<IFormFile>? images)
        {
                try
                {
                    await _usedBookRepository.BeginTransactionAsync();
                var isUserExist = await _userRepository.GetByIdAsync(book.UserId);
                if(isUserExist is null)
                    throw new Exception("is User is Not Exist.");

                // Step 1: Add Book
                var addedBook = await _usedBookRepository.AddAsync(book);
                    if (addedBook is null)
                    {
                        throw new Exception(SystemMessages.FailedToAddEntity);
                    }

                    // Step 2: Upload Primary Image
                    var primaryUploadResult = await _imageUploaderService.UploadImageAsync(primaryImage, ImageFolder.BookPrimaryImage);
                    if (primaryUploadResult == null || string.IsNullOrEmpty(primaryUploadResult.Url.ToString()))
                    {
                        throw new Exception("Primary image upload failed.");
                    }

                    var primaryImageEntity = new Image
                    {
                        Url = primaryUploadResult.Url.ToString(),
                        IsPrimary = true
                    };

                    var primaryBookImage = new BookImages
                    {
                        Book = addedBook,
                        Image = primaryImageEntity
                    };

                    await _bookImageService.AddBookImageAsync(primaryBookImage);

                    // Step 3: Upload Additional Images (if any)
                    if (images != null && images.Any())
                    {
                        foreach (var file in images)
                        {
                            if (file == null || file.Length == 0)
                                continue; // Skip invalid files

                            var imageResult = await _imageUploaderService.UploadImageAsync(file, ImageFolder.BookImage);
                            if (imageResult == null || string.IsNullOrEmpty(imageResult.Url.ToString()))
                            {
                                _logger.LogWarning("Skipping image due to failed upload for file: {FileName}", file.FileName);
                                continue; // Skip this file and continue with others
                            }

                            var imageEntity = new Image
                            {
                                Url = imageResult.Url.ToString(),
                                IsPrimary = false
                            };

                            var bookImage = new BookImages
                            {
                                Book = addedBook,
                                Image = imageEntity
                            };

                            await _bookImageService.AddBookImageAsync(bookImage);
                        }
                    }

                    await _usedBookRepository.CommitAsync();
                    return SystemMessages.Success;
                }
                catch (Exception ex)
                {
                    await _usedBookRepository.RollBackAsync();
                    _logger.LogError(ex, "Failed to create used book. Book ID: {BookId}", book.Id);
                    throw new Exception(SystemMessages.FailedToAddEntity, ex);
                }
            }


        public async Task<UsedBook> GetUsedBookByIdAsync(Guid id, bool asTracked = false)
        {
            var usedBook = await _usedBookRepository.GetByIdAsync(id, false);
            return usedBook ?? throw new KeyNotFoundException($"Used book with ID {id} not found");
        }

        public async Task<IEnumerable<UsedBook>> GetUsedBooksByConditionAsync(UsedBookCondition condition)
        {
            if (!Enum.IsDefined(typeof(UsedBookCondition), condition))
                throw new ArgumentException("Invalid book condition");

            return await _usedBookRepository.GetUsedBooksByConditionAsync(condition);
        }

        public async Task<IEnumerable<UsedBook>> GetUsedBooksByOwnerAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID must be specified");

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found");

            return await _usedBookRepository.GetUsedBooksByOwnerAsync(userId);
        }

        public async Task<IQueryable<UsedBook>> SearchUsedBooks(string searchTerm)
        {
            var usedBooks = await _usedBookRepository.SearchUsedBooksAsync(searchTerm);
            return usedBooks.AsQueryable();
        }


        public async Task<bool> UpdateUsedBookAsync(UsedBook usedBook, IFormFile? primaryImage, ICollection<IFormFile>? images)
        {
            try
            {
                await _usedBookRepository.BeginTransactionAsync();

                var bookId = usedBook.Id;
                await _usedBookRepository.UpdateAsync(usedBook);

                var existingImages = await _bookImageService.GetImagesForBookAsync(bookId);

                // Update primary image
                if (primaryImage != null)
                {
                    var existingPrimary = existingImages.FirstOrDefault(i => i.Image.IsPrimary);
                    if (existingPrimary != null)
                    {
                        if (!string.IsNullOrEmpty(existingPrimary.Image.Url))
                            await _imageUploaderService.DeleteImageByUrlAsync(existingPrimary.Image.Url);

                        await _bookImageService.DeleteImageAsync(existingPrimary);
                    }

                    var uploadResult = await _imageUploaderService.UploadImageAsync(primaryImage, ImageFolder.BookPrimaryImage);
                    if (uploadResult == null || string.IsNullOrEmpty(uploadResult.Url.ToString()))
                        throw new Exception("Failed to upload primary image.");

                    var newPrimaryImage = new Image { Url = uploadResult.Url.ToString(), IsPrimary = true };

                    var newPrimaryBookImage = new BookImages
                    {
                        BookId = bookId,
                        Image = newPrimaryImage
                    };

                    await _bookImageService.AddBookImageAsync(newPrimaryBookImage);
                }

                // Update additional images
                if (images != null && images.Any())
                {
                    var oldImages = existingImages.Where(i => !i.Image.IsPrimary).ToList();
                    foreach (var img in oldImages)
                    {
                        if (!string.IsNullOrEmpty(img.Image.Url))
                            await _imageUploaderService.DeleteImageByUrlAsync(img.Image.Url);

                        await _bookImageService.DeleteImageAsync(img);
                    }

                    foreach (var file in images)
                    {
                        if (file == null || file.Length == 0)
                            continue;

                        var uploadResult = await _imageUploaderService.UploadImageAsync(file, ImageFolder.BookImage);
                        if (uploadResult == null || string.IsNullOrEmpty(uploadResult.Url.ToString()))
                        {
                            _logger.LogWarning("Skipping additional image due to failed upload: {FileName}", file.FileName);
                            continue;
                        }

                        var image = new Image { Url = uploadResult.Url.ToString(), IsPrimary = false };

                        var bookImage = new BookImages
                        {
                            BookId = bookId,
                            Image = image
                        };

                        await _bookImageService.AddBookImageAsync(bookImage);
                    }
                }

                await _usedBookRepository.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _usedBookRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to update used book with ID {BookId}", usedBook.Id);
                throw new Exception(SystemMessages.FailedToUpdateEntity, ex);
            }
        }


        public async Task<bool> DeleteUsedBookAsync(UsedBook book)
        {
            try
            {
                await _usedBookRepository.BeginTransactionAsync();

                var bookImages = await _bookImageService.GetImagesForBookAsync(book.Id);

                //Delete book record
                await _usedBookRepository.DeleteAsync(book);

                //Delete images from Cloud
                foreach (var item in bookImages)
                {
                    if (!string.IsNullOrEmpty(item.Image.Url))
                    {
                        await _imageUploaderService.DeleteImageByUrlAsync(item.Image.Url);
                    }
                }

                await _usedBookRepository.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _usedBookRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to delete used book with ID {BookId}", book.Id);
                throw new Exception(SystemMessages.FailedToDeleteEntity, ex);
            }
        }

    }
}
