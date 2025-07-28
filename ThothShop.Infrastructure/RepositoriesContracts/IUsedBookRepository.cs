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
    public interface IUsedBookRepository :IGenericRepository<UsedBook>
    {
        Task<IEnumerable<UsedBook>> GetUsedBooksByConditionAsync(UsedBookCondition condition);
        Task<IEnumerable<UsedBook>> GetUsedBooksByOwnerAsync(string userId);
        Task<IEnumerable<UsedBook>> SearchUsedBooksAsync(string searchTerm);
        Task<int> GetUsedBookCountByConditionAsync(UsedBookCondition condition);
        Task<bool> UpdateUsedBookConditionAsync(Guid usedBookId, UsedBookCondition newCondition);
    }
}
