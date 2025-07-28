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
    public class AuthormageRepository : GenericRepository<AuthorImages> , IAuthorImageRepository
    {
        private readonly ApplicationDBContext _context;
        public AuthormageRepository(ApplicationDBContext context) : base(context)
        {
           _context = context?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<AuthorImages>> GetAllImagesByAuthorAsync(Guid bookId)
        {
            return await _context.AuthorImages.Include(bkIm => bkIm.Image)
                .Where(bkIm => bkIm.author.Id == bookId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AuthorImages?> GetPrimaryImageForAuthorAsync(Guid bookId)
        {
            return await _context.AuthorImages
                .Include(bkIm => bkIm.Image)
                .Where(bkIm => bkIm.author.Id == bookId && bkIm.Image.IsPrimary)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

    

    }
}
