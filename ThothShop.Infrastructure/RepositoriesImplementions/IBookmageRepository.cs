using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.Contracts;

namespace ThothShop.Infrastructure.Implementions
{
    public class BookmageRepository:GenericRepository<BookImages> , IBookImageRepository
    {
        private readonly ApplicationDBContext _context;
        public BookmageRepository(ApplicationDBContext context) : base(context)
        {
           _context = context?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<BookImages>> GetAllImagesByBookAsync(Guid bookId)
        {
            return await _context.BookImages.Include(bkIm => bkIm.Image)
                .Where(bkIm => bkIm.Book.Id == bookId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BookImages?> GetPrimaryImageForBookAsync(Guid bookId)
        {
            return await _context.BookImages
                .Include(bkIm => bkIm.Image)
                .Where(bkIm => bkIm.Book.Id == bookId && bkIm.Image.IsPrimary)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

    }
}
