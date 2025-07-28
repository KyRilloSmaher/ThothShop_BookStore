using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.Contracts
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        public Task<Author?> GetByIdAsync(Guid id, bool AsTracking = false);
        public Task<bool> IsAuthorExist(Guid id);
        public Task<IEnumerable<Author>> SearchAuthorsByNameAsync(string name);
        public Task<IEnumerable<Author>> GetAuthorsByBookAsync( Guid  bookId );
        public Task<bool> IncrementAuthorViewCountAsync(Guid authorId);
        public Task<int> GetAuthorsCount();
        public Task<(IEnumerable<Author>,int)> GetAuthorsByViewCountDescendingAsync(int PageNumber, int PageSize);
        Task<IEnumerable<Author>> GetAuthorsByNationalityAsync(Nationality nationality);
        Task<IEnumerable<Author>> GetAllAuthorsByPaginationAsync(int PageNumber, int PageSize);
        IQueryable<Author> GetFilteredAuthors(FilterAuthorModel filterModel, int PageNumber = 1, int PageSize = 10);
    }
}
