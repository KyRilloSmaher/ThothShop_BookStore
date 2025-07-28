using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Service.Contract
{
    public interface IAuhtorService
    {
        Task<string> CreateAuthorAsync(Author author , IFormFile primaryImage, IList<IFormFile> images);
        Task<Author?> GetAuthorByIdAsync(Guid id, bool tracked =false);
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<IEnumerable<Author>> GetAuthorsByNationalityAsync(Nationality nationality);
        Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name);
        Task<int> GetAuthorsCountAsync();
        Task<bool> UpdateAuthorAsync(Author author , IFormFile? primaryImage, IList<IFormFile>? images);
        Task<bool> DeleteAuthorAsync(Author author);
        Task<int> GetAuthorViewCountAsync(Guid authorId);
        Task<bool> IsAuthorExist(Guid authorId);
        Task<IEnumerable<Author>> GetAllAuthorsByPaginationAsync(int PageNumber, int PageSize);
        IQueryable<Author> FilterAuthors(FilterAuthorModel filterModel, int PageNumber = 1, int PageSize = 10);
        Task<(IEnumerable<Author>, int)> GetPopularAuthorsAsync(int PageNumber, int PageSize);
    }
}

