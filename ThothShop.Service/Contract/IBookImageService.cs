using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;

namespace ThothShop.Service.Contract
{
    public interface IBookImageService
    {

        public  Task<string> AddBookImageAsync(BookImages bookImage);
     

        public  Task<bool> SetPrimaryImageAsync(Guid imageId);
    
        public Task<IEnumerable<BookImages>> GetImagesForBookAsync(Guid bookId);
      

        public Task<bool> DeleteImageAsync(BookImages image);
    

        public Task<bool> DeleteAllImagesForBookAsync(Guid bookId);


        public Task ClearPrimaryImageFlag(Guid bookId);

      }
    }
