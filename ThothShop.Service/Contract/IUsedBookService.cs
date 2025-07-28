using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;

namespace ThothShop.Service.Contract
{
    public interface IUsedBookService
    {
        Task<IEnumerable<UsedBook>> GetAllUsedBooksAsync();
        Task<string> CreateUsedBookAsync(UsedBook usedBook,IFormFile primaryImage , ICollection<IFormFile>?Images);
        Task<UsedBook> GetUsedBookByIdAsync(Guid id, bool asTracked = false);
        Task<IEnumerable<UsedBook>> GetUsedBooksByConditionAsync(UsedBookCondition condition);
        Task<IEnumerable<UsedBook>> GetUsedBooksByOwnerAsync(string userId);
        Task<bool> UpdateUsedBookAsync(UsedBook usedBook, IFormFile? primaryImage, ICollection<IFormFile>? Images);
        Task<bool> DeleteUsedBookAsync(UsedBook usedBook);
        Task<IQueryable<UsedBook>> SearchUsedBooks(string searchTerm);
    }
}
