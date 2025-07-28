using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.Contracts;

namespace ThothShop.Infrastructure.Implementions
{
    public class UsedBookRepository : GenericRepository<UsedBook> ,IUsedBookRepository
    {
        private readonly ApplicationDBContext _context;
        public UsedBookRepository(ApplicationDBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }
        public override async Task<UsedBook?> GetByIdAsync(Guid id, bool tracked)
        {
            return tracked ? await _context.usedBooks.Include(ub => ub.user).Include(ub => ub.BookImages).ThenInclude(BI =>BI.Image).Include(ub => ub.Category).FirstOrDefaultAsync(ub => ub.Id == id):
                             await _context.usedBooks.AsNoTracking().Include(ub => ub.user).Include(ub => ub.Category).Include(ub => ub.BookImages).ThenInclude(BI => BI.Image).FirstOrDefaultAsync(ub => ub.Id == id);
        }
    

        public async Task<int> GetUsedBookCountByConditionAsync(UsedBookCondition condition)
        {
            return await _context.usedBooks.CountAsync(ub => ub.Condition == condition);
        }

        public async Task<IEnumerable<UsedBook>> GetUsedBooksByConditionAsync(UsedBookCondition condition)
        {
            return await _context.usedBooks.Where(ub => ub.Condition == condition).Include(ub => ub.BookImages).ThenInclude(BI => BI.Image).ToListAsync();
        }

        public async Task<IEnumerable<UsedBook>> GetUsedBooksByOwnerAsync(string userId)
        {
            return await _context.usedBooks.Where(ub => ub.UserId == userId).Include(ub => ub.BookImages).ThenInclude(BI => BI.Image).Include(ub => ub.user).Include(ub=>ub.Category).ToListAsync();
        }

        public async  Task<IEnumerable<UsedBook>> SearchUsedBooksAsync(string searchTerm)
        {
            return await _context.usedBooks
                .Include(ub => ub.user).Include(ub => ub.Category)
              .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
             .Where(ub => EF.Functions.Like(ub.Title, $"%{searchTerm}%") ||
                         EF.Functions.Like(ub.Comment, $"%{searchTerm}%"))
             .AsNoTracking()
             .ToListAsync();
        }

        public async Task<bool> UpdateUsedBookConditionAsync(Guid usedBookId, UsedBookCondition newCondition)
        {
            var usedBook = await _context.usedBooks.FindAsync(usedBookId);
            if (usedBook == null) return false;

            usedBook.Condition = newCondition;
            await _context.SaveChangesAsync();
            return true;
        }

        public override async Task<IEnumerable<UsedBook>> GetAll()
        {
            return await _dbContext.usedBooks.Include(ub => ub.BookImages).ThenInclude(BI => BI.Image).Include(ub => ub.user).Include(ub => ub.Category).AsNoTracking().ToListAsync();
        }
    }
}
