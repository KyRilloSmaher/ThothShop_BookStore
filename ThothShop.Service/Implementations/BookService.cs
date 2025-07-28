using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Service.Commans;
using ThothShop.Service.Contract;
using ThothShop.Service.Wrappers;

namespace ThothShop.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IImageUploaderService _imageUploaderService;
        private readonly ILogger<BookService> _logger;
        private readonly IBookImageService _bookImageService;

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IImageUploaderService imageUploaderService,
            IBookImageService bookImageService,
            ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _imageUploaderService = imageUploaderService;
            _bookImageService = bookImageService;
            _logger = logger;
        }

        // -------------------------
        // CRUD Operations

        public async Task<string> CreateBookAsync(Book book, IFormFile primaryImage, List<IFormFile> images)
        {
            try
            {
                // Begin a transaction.
                await _bookRepository.BeginTransactionAsync();

                // Add the new book.
                var addedBook = await _bookRepository.AddAsync(book);
                if (addedBook is null)
                {
                    throw new Exception(SystemMessages.FailedToAddEntity);
                }

                // Process primary image.
                var primaryResult = await _imageUploaderService.UploadImageAsync(primaryImage, ImageFolder.BookPrimaryImage);
                var primaryImageUrl = primaryResult.Url.ToString();
                var primaryImageEntity = new Image
                {
                    Url = primaryImageUrl,
                    IsPrimary = true
                };

                var primaryBookImage = new BookImages
                {
                    Book = addedBook,
                    Image = primaryImageEntity
                };
                await _bookImageService.AddBookImageAsync(primaryBookImage);

                // Process additional images.
                if (images != null && images.Any())
                {
                    foreach (var file in images)
                    {
                        var result = await _imageUploaderService.UploadImageAsync(file, ImageFolder.BookImage);
                        var imageUrl = result.Url.ToString();
                        var imageEntity = new Image
                        {
                            Url = imageUrl,
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

                await _bookRepository.CommitAsync();
                return SystemMessages.Success;
            }
            catch (Exception ex)
            {
                await _bookRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to create book with ID {BookId}", book.Id);
                throw new Exception(SystemMessages.FailedToAddEntity, ex);
            }
        }


        public async Task<bool> UpdateBookAsync(Book book, IFormFile? primaryImage, IList<IFormFile>? images)
        {
            try
            {
                await _bookRepository.BeginTransactionAsync();
                var bookId = book.Id;

                // Update the book details.
                await _bookRepository.UpdateAsync(book);

                // Get the current images for the book.
                var existingImages = await _bookImageService.GetImagesForBookAsync(bookId);

                // Process primary image update if a new one is uploaded.
                if (primaryImage != null)
                {
                    var existingPrimary = existingImages.FirstOrDefault(i => i.Image.IsPrimary);
                    if (existingPrimary != null)
                    {
                        await _imageUploaderService.DeleteImageByUrlAsync(existingPrimary.Image.Url);
                        await _bookImageService.DeleteImageAsync(existingPrimary);
                    }

                    var primaryResult = await _imageUploaderService.UploadImageAsync(primaryImage, ImageFolder.BookPrimaryImage);
                    var primaryImageUrl = primaryResult.Url.ToString();
                    var primaryImageEntity = new Image
                    {
                        Url = primaryImageUrl,
                        IsPrimary = true
                    };

                    var primaryBookImage = new BookImages
                    {
                        BookId = bookId,
                        Image = primaryImageEntity
                    };
                    await _bookImageService.AddBookImageAsync(primaryBookImage);
                }

                // Process update of additional images.
                if (images != null && images.Any())
                {
                    var existingOthers = existingImages.Where(i => !i.Image.IsPrimary).ToList();
                    foreach (var img in existingOthers)
                    {
                        await _imageUploaderService.DeleteImageByUrlAsync(img.Image.Url);
                        await _bookImageService.DeleteImageAsync(img);
                    }

                    foreach (var file in images)
                    {
                        var result = await _imageUploaderService.UploadImageAsync(file, ImageFolder.BookImage);
                        var imageUrl = result.Url.ToString();
                        var imageEntity = new Image
                        {
                            Url = imageUrl,
                            IsPrimary = false
                        };

                        var bookImage = new BookImages
                        {
                            BookId = bookId,
                            Image = imageEntity
                        };
                        await _bookImageService.AddBookImageAsync(bookImage);
                    }
                }

                await _bookRepository.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _bookRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to update book with ID {BookId}", book.Id);
                throw new Exception(SystemMessages.FailedToUpdateEntity, ex);
            }
        }

        public async Task<bool> DeleteBookAsync(Book book)
        {
            try
            {
                await _bookRepository.BeginTransactionAsync();
                var bookId = book.Id;

                var bookImages = await _bookImageService.GetImagesForBookAsync(bookId);
                await _bookRepository.DeleteAsync(book);

                // Delete images associated with the book.
                foreach (var img in bookImages)
                {
                    await _imageUploaderService.DeleteImageByUrlAsync(img.Image.Url);
                }

                await _bookRepository.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _bookRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to delete book with ID {BookId}", book.Id);
                throw new Exception(SystemMessages.FailedToAddEntity, ex);
            }
        }

        // -------------------------
        // Single Item Operations

        public async Task<Book?> GetBookByIdAsync(Guid id, bool tracked = false)
        {
            return await _bookRepository.GetByIdAsync(id, tracked);
        }

        public async Task<Book?> GetBookByIdIncludingCategoryAsNotrackingAsync(Guid id, bool tracked = false)
        {
            // Not tracking by default if inclusion is required.
            return await _bookRepository.GetByIdAsync(id, false);
        }

        public async Task<decimal> GetAverageBookRatingAsync(Guid bookId)
        {
            return await _bookRepository.GetAverageBookRatingAsync(bookId);
        }

        public async Task<bool> IsBookIdExits(Guid bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            return book != null;
        }

        public async Task<bool> IncreaseBookStock(Guid bookId, int amount)
        {
            var book = await GetBookByIdAsync(bookId, true);
            if (book is null)
            {
                return false;
            }
            return await _bookRepository.IncreaseBookStockAmount(book, amount);
        }

        public async Task<bool> DecreaseBookStock(Guid bookId, int amount)
        {
            var book = await GetBookByIdAsync(bookId, true);
            if (book is null)
            {
                return false;
            }
            return await _bookRepository.DecreaseBookStockAmount(book, amount);
        }

        public async Task<int> GetTotalBooksCountAsync()
        {
            return await _bookRepository.GetTotalBooksCountAsync();
        }

        // -------------------------
        // Collection Operations

        public async Task<IQueryable<Book>> GetBooksByAuthorAsync(Guid authorId)
        {
            return await _bookRepository.GetBooksByAuthorAsync(authorId);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorOrderedByViewsAsync(Guid authorId)
        {
            return await _bookRepository.GetBooksByAuthorOrderedByViewsAsync(authorId);
        }

        public Task<IQueryable<Book>> GetBooksByCategoryAsync(Guid categoryId)
        {
            return Task.FromResult(_bookRepository.GetBooksByCategory(categoryId));
        }

        public Task<IQueryable<Book>> GetBooksOrderedByStockAscendingAsync()
        {
            return Task.FromResult(_bookRepository.GetBooksOrderedByStockAscendingAsync());
        }

        public Task<IQueryable<Book>> GetBooksOrderedByStockDescendingAsync()
        {
            return Task.FromResult(_bookRepository.GetBooksOrderedByStockDescendingAsync());
        }

        public async Task<IEnumerable<Book>> GetOutOfStockBooksAsync()
        {
            return await _bookRepository.GetOutOfStockBooksAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAndCategoryAsync(Guid authorId, Guid categoryId)
        {
            return await _bookRepository.GetBooksByAuthorAndCategoryAsync(authorId, categoryId);
        }

        public async Task<IEnumerable<Author>> GetAuthorsForBookAsync(Guid bookId)
        {
            return await _authorRepository.GetAuthorsByBookAsync(bookId);
        }

        // -------------------------
        // Pagination-Ready IQueryable Queries

        public IQueryable<Book> GetNewReleases()
        {
            return _bookRepository.GetNewReleases();
        }

        public IQueryable<Book> GetTopRatedBooks()
        {
            return _bookRepository.GetTopRatedBooks();
        }

        public IQueryable<Book> GetTopSellingBooks()
        {
            return _bookRepository.GetTopSellingBooks();
        }

        public IQueryable<Book> GetSimilarBooks(Guid bookId)
        {
            return _bookRepository.GetSimilarBooks(bookId);
        }

        public IQueryable<Book> GetLowStockBooks(int threshold = 5)
        {
            return _bookRepository.GetLowStockBooks(threshold);
        }

        public Task<IQueryable<Book>> SearchBooksQueryable(FilterListParams model)
        {
            Expression<Func<Book, bool>> predicate = book => true;

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                predicate = predicate.And(book =>
                    book.Title.Contains(model.Name) || book.Description.Contains(model.Name));
            }

            if (model.PublishedDate != default(DateTime))
            {
                predicate = predicate.And(book => book.PublishedDate.Date == model.PublishedDate.Date);
            }

            if (model.Price != default(decimal))
            {
                predicate = predicate.And(book => (book.Price <=Math.Ceiling(model.Price) && book.Price >= Math.Floor(model.Price)));
            }

            Expression<Func<Book, decimal>> orderBy = book => book.Price;
            bool ascending = true;

            return _bookRepository.SearchBooksQueryable(orderBy, predicate, ascending);
        }


        public async Task<IQueryable<Book>> GetAllBooksAsync()
        {
            return await Task.FromResult(_bookRepository.GetAll());
        }
    }
}
