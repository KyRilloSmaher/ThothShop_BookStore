using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Service.Contract;

namespace ThothShop.Service.implementations
{
    public class BookImageService : IBookImageService
    {
        private readonly IBookImageRepository _imageRepository;

        public BookImageService(
            IBookImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<string> AddBookImageAsync(BookImages bookImage)
        {
             await _imageRepository.AddAsync(bookImage);
             return SystemMessages.Success;
        }

        public async Task<bool> SetPrimaryImageAsync(Guid imageId)
        {
            var image = await _imageRepository.GetByIdAsync(imageId);
            if (image == null)
                return false;

            await ClearPrimaryImageFlag(image.BookId);

            image.Image.IsPrimary = true;
            await _imageRepository.UpdateAsync(image);
            await _imageRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<BookImages>> GetImagesForBookAsync(Guid bookId)
        {
            return await _imageRepository.GetAllImagesByBookAsync(bookId);
        }

        public async Task<bool> DeleteImageAsync(BookImages image)
        {
          
            await _imageRepository.DeleteAsync(image);
            return true;
        }

        public async Task<bool> DeleteAllImagesForBookAsync(Guid bookId)
        {
            var images = await _imageRepository.GetAllImagesByBookAsync(bookId);
            if (!images.Any())
                return true;

     

            // Delete all from database
            await _imageRepository.DeleteRangeAsync(images.ToList());
            await _imageRepository.SaveChangesAsync();

            return true;
        }

        public async Task ClearPrimaryImageFlag(Guid bookId)
        {
            var currentPrimary = await _imageRepository.GetPrimaryImageForBookAsync(bookId);
            if (currentPrimary != null)
            {
                currentPrimary.Image.IsPrimary = false;
                await _imageRepository.UpdateAsync(currentPrimary);
            }
        }

    }
}
