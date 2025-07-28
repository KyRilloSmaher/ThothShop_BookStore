using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Infrastructure.Implementations;
using ThothShop.Service.Contract;
using ThothShop.Service.Implementations;

namespace ThothShop.Service.implementations
{
    public class AuhtorService : IAuhtorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IImageUploaderService _imageUploaderService;
        private readonly IAuthorImageRepository _AuthorImageService;
        private readonly ILogger<AuhtorService> _logger;
        public AuhtorService(IAuthorRepository authorRepository, IImageUploaderService imageUploaderService, IAuthorImageRepository authorImageService, ILogger<AuhtorService> logger)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _imageUploaderService = imageUploaderService;
            _AuthorImageService = authorImageService;
            _logger = logger;
        }

        public async Task<string> CreateAuthorAsync(Author author, IFormFile primaryImage, IList<IFormFile> images)
        {
            try
            {
                // Begin a transaction.
                await _authorRepository.BeginTransactionAsync();

                // Add the new Author.
                var addedAuthor = await _authorRepository.AddAsync(author);
                if (addedAuthor is null)
                {
                    throw new Exception(SystemMessages.FailedToAddEntity);
                }

                // Process primary image.
                var primaryResult = await _imageUploaderService.UploadImageAsync(primaryImage, ImageFolder.AuthorImage);
                var primaryImageUrl = primaryResult.Url.ToString();
                var primaryImageEntity = new Image
                {
                    Url = primaryImageUrl,
                    IsPrimary = true
                };

                var primaryAuthorImage = new AuthorImages
                {
                    author = addedAuthor,
                    Image = primaryImageEntity
                };
                await _AuthorImageService.AddAsync(primaryAuthorImage);

                // Process additional images.
                if (images != null && images.Any())
                {
                    foreach (var file in images)
                    {
                        var result = await _imageUploaderService.UploadImageAsync(file, ImageFolder.AuthorImage);
                        var imageUrl = result.Url.ToString();
                        var imageEntity = new Image
                        {
                            Url = imageUrl,
                            IsPrimary = false
                        };

                        var AuthorImage = new AuthorImages
                        {
                            author = addedAuthor,
                            Image = imageEntity
                        };
                        await _AuthorImageService.AddAsync(AuthorImage);
                    }
                }

                await _authorRepository.CommitAsync();
                return string.Format(SystemMessages.CreatedSuccessfully, "Auhtor");
            }
            catch (Exception ex)
            {
                await _authorRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to create Author with ID {AuthorId}", author.Id);
                throw new Exception(SystemMessages.FailedToAddEntity, ex);
            }
         
        }
        public async Task<bool> UpdateAuthorAsync(Author author, IFormFile? primaryImage, IList<IFormFile>? images)
        {

            try
            {
                await _authorRepository.BeginTransactionAsync();
                var AuthorId = author.Id;

                // Update the Author details.
                await _authorRepository.UpdateAsync(author);

                // Get the current images for the Author.
                var existingImages = await _AuthorImageService.GetAllImagesByAuthorAsync(AuthorId);

                // Process primary image update if a new one is uploaded.
                if (primaryImage != null)
                {
                    var existingPrimary = existingImages.FirstOrDefault(i => i.Image.IsPrimary);
                    if (existingPrimary != null)
                    {
                        await _imageUploaderService.DeleteImageByUrlAsync(existingPrimary.Image.Url);
                        await _AuthorImageService.DeleteAsync(existingPrimary);
                    }

                    var primaryResult = await _imageUploaderService.UploadImageAsync(primaryImage, ImageFolder.AuthorImage);
                    var primaryImageUrl = primaryResult.Url.ToString();
                    var primaryImageEntity = new Image
                    {
                        Url = primaryImageUrl,
                        IsPrimary = true
                    };

                    var primaryAuthorImage = new AuthorImages
                    {
                        AuthorId = AuthorId,
                        Image = primaryImageEntity
                    };
                    await _AuthorImageService.AddAsync(primaryAuthorImage);
                }

                // Process update of additional images.
                if (images != null && images.Any())
                {
                    var existingOthers = existingImages.Where(i => !i.Image.IsPrimary).ToList();
                    foreach (var img in existingOthers)
                    {
                        await _imageUploaderService.DeleteImageByUrlAsync(img.Image.Url);
                        await _AuthorImageService.DeleteAsync(img);
                    }

                    foreach (var file in images)
                    {
                        var result = await _imageUploaderService.UploadImageAsync(file, ImageFolder.AuthorImage);
                        var imageUrl = result.Url.ToString();
                        var imageEntity = new Image
                        {
                            Url = imageUrl,
                            IsPrimary = false
                        };

                        var AuthorImage = new AuthorImages
                        {
                            AuthorId = AuthorId,
                            Image = imageEntity
                        };
                        await _AuthorImageService.AddAsync(AuthorImage);
                    }
                }

                await _authorRepository.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _authorRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to delete Author with ID {authorId}", author.Id);
                throw new Exception(SystemMessages.FailedToUpdateEntity, ex);
            }
        }
        public async Task<bool> DeleteAuthorAsync(Author author)
        {
            try
            {
                await _authorRepository.BeginTransactionAsync();
                var authorId = author.Id;

                var AuthorImages = await _AuthorImageService.GetAllImagesByAuthorAsync(authorId);
                await _authorRepository.DeleteAsync(author);

                // Delete images associated with the Author.
                foreach (var img in AuthorImages)
                {
                    await _imageUploaderService.DeleteImageByUrlAsync(img.Image.Url);
                }

                await _authorRepository.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _authorRepository.RollBackAsync();
                _logger.LogError(ex, "Failed to delete Author with ID {authorId}", author.Id);
                throw new Exception(SystemMessages.FailedToAddEntity, ex);
            }
        }
        public async Task<int> GetAuthorsCountAsync() {
            return await _authorRepository.GetAuthorsCount();
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsByPaginationAsync(int PageNumber, int PageSize)
        {
            return await _authorRepository.GetAllAuthorsByPaginationAsync(PageNumber, PageSize);
        }


        public async Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name)
        {
            var authors = await _authorRepository.SearchAuthorsByNameAsync(name);

            return authors;
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNationalityAsync(Nationality nationality)
        {
            return await _authorRepository.GetAuthorsByNationalityAsync(nationality);
        }

        public async Task<int> GetAuthorViewCountAsync(Guid authorId)
        {
            var Author = await _authorRepository.GetByIdAsync(authorId,false);
            int AuthorViewCount = Author.ViewCount;
            return AuthorViewCount;
        }

        public async Task<(IEnumerable<Author>, int)> GetPopularAuthorsAsync(int PageNumber, int PageSize)
        {
            return await _authorRepository.GetAuthorsByViewCountDescendingAsync(PageNumber, PageSize);
        }
        public IQueryable<Author> FilterAuthors(FilterAuthorModel filterModel, int PageNumber =1, int PageSize = 10)
        {
           

            return  _authorRepository.GetFilteredAuthors(filterModel, PageNumber, PageSize);
        }

        public async Task<Author?> GetAuthorByIdAsync(Guid id, bool tracked =false)
        {
            return await _authorRepository.GetByIdAsync(id,tracked);
        }

        public async Task<bool> IsAuthorExist(Guid authorId)
        {
          return await _authorRepository.IsAuthorExist(authorId);
        }
    }
}
