using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Threading;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.Contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ThothShop.Infrastructure.Implementations
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationDBContext _context;

        public AuthorRepository(ApplicationDBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public override async Task<Author?> GetByIdAsync(Guid id, bool tracked = false)
        {
          return tracked ? await  _context.Authors.Include(A=>A.authorCategories).ThenInclude(ac => ac.Category).Include(A=>A.AuthorImages).ThenInclude(ai=>ai.Image).FirstOrDefaultAsync(a=>a.Id == id) :
                           await _context.Authors.Include(A => A.authorCategories).ThenInclude(ac => ac.Category).Include(A => A.AuthorImages).ThenInclude(ai => ai.Image).AsNoTracking().FirstOrDefaultAsync(a=>a.Id ==id);
        }
        public async Task<(IEnumerable<Author>,int)> GetAuthorsByViewCountDescendingAsync(int PageNumber, int PageSize)
        {
            return( await _context.Authors.Include(A => A.authorCategories).ThenInclude(ac => ac.Category).Include(A => A.AuthorImages).ThenInclude(ai => ai.Image)
                .OrderByDescending(a => a.ViewCount)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .AsNoTracking()
                .ToListAsync(),await _context.Authors.CountAsync());
        }

        public async Task<IEnumerable<Author>> GetAuthorsByBookAsync(Guid bookId)
        {
            return await _context.BookAuthors
                .Where(ba => ba.BookId == bookId)
                .Include(ba => ba.Author).ThenInclude(A => A.authorCategories).ThenInclude(ac => ac.Category)
                .Include(ba => ba.Author).ThenInclude(A => A.AuthorImages).ThenInclude(ai => ai.Image)
                .Select(ba => ba.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNationalityAsync(Nationality nationality)
        {
            return await _context.Authors.Include(A => A.authorCategories).ThenInclude(ac => ac.Category).Include(A => A.AuthorImages).ThenInclude(ai => ai.Image)
                .Where(a => a.Nationality == nationality)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> IncrementAuthorViewCountAsync(Guid authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null) return false;

            author.ViewCount++;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Author>> SearchAuthorsByNameAsync(string name)
        {
            return await _context.Authors
                .Where(a => EF.Functions.Like(a.Name, $"%{name}%"))
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<int> GetAuthorsCount() { 
         
            var result = await _context.Authors.CountAsync();
            return result;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsByPaginationAsync(int PageNumber, int PageSize)
        {
            return await _context.Authors.Include(A => A.authorCategories).ThenInclude(ac => ac.Category).Include(A => A.AuthorImages).ThenInclude(ai => ai.Image)
                             .Skip((PageNumber - 1) * PageSize)
                             .Take(PageSize)
                             .AsNoTracking()
                             .ToListAsync();
        }

        public override async Task<IEnumerable<Author>> GetAll()
        {
            return await _context.Authors.Include(A => A.authorCategories).ThenInclude(ac=>ac.Category).Include(A => A.AuthorImages).ThenInclude(ai => ai.Image).AsNoTracking().ToListAsync();
        }
        public IQueryable<Author> GetFilteredAuthors(FilterAuthorModel filter, int pageNumber, int pageSize)
        {
            var query = _context.Authors.Include(A => A.authorCategories).ThenInclude(ac=>ac.Category).Include(A => A.AuthorImages).ThenInclude(ai => ai.Image).AsQueryable();
            
            // Apply filters
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(a => a.Name.Contains(filter.SearchTerm) || a.Bio.Contains(filter.SearchTerm));
            }
            if (filter.Nationality is not null)
            {
                query = query.Where(a => a.Nationality == filter.Nationality);
            }

            // Paging
            query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();

            return query;

        }
        public async Task<bool> IsAuthorExist(Guid id)
        {
           return await base.GetByIdAsync(id) != null;
        }
    }
    }