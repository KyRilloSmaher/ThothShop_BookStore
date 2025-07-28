using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.Contracts;


namespace ThothShop.Infrastructure.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDBContext _context;

        public BookRepository(ApplicationDBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IQueryable<Book> GetBooksByCategory(Guid categoryId)
        {
            return _context.Books
                .Where(b => b.CategoryId == categoryId)
                .Include(b => b.Category)
                .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                .AsNoTracking()
                .AsQueryable();
        }

        public Task<IQueryable<Book>> GetBooksByAuthorAsync(Guid authorId)
        {
            return Task.FromResult(_context.Books
                                 .Include(b => b.Category)
                                 .Join(_context.BookAuthors,
                                     book => book.Id,
                                     bookAuthor => bookAuthor.BookId,
                                     (book, bookAuthor) => new { Book = book, BookAuthor = bookAuthor })
                                 .Where(x => x.BookAuthor.AuthorId == authorId)
                                 .Select(x => x.Book)
                                    .Include(b => b.Category)
                                .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                                 .AsNoTracking().AsQueryable());
        
                
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorOrderedByViewsAsync(Guid authorId)
        {
            return await _context.Books
                                .Join(_context.BookAuthors,
                                    book => book.Id,
                                    bookAuthor => bookAuthor.BookId,
                                    (book, bookAuthor) => new { Book = book, BookAuthor = bookAuthor })
                                .Where(x => x.BookAuthor.AuthorId == authorId)
                                .OrderByDescending(x => x.Book.ViewCount)
                                .Select(x => x.Book)
                                .Include(b => b.Category)
                                .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                                .AsNoTracking().ToListAsync();
        }

        public IQueryable<Book> GetBooksOrderedByStockAscendingAsync()
        {
            return  _context.Books
                .OrderBy(b => b.Stock)
                .Include(b => b.Category)
                .AsNoTracking()
                .AsQueryable();
        }

        public  IQueryable<Book> GetBooksOrderedByStockDescendingAsync()
        {
            return  _context.Books
                .OrderByDescending(b => b.Stock)
                .Include(b=>b.Category)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<IEnumerable<Book>> GetOutOfStockBooksAsync() {

            return await _context.Books
                .Where(b => b.Stock == 0)
                .Include(b => b.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public IQueryable<Book> GetNewReleases()
        {
            return _context.Books
                .OrderByDescending(b => b.PublishedDate)
                 .Include(b => b.Category)
                 .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                .AsNoTracking();
        }

        public IQueryable<Book> GetTopRatedBooks()
        {
            var topBookIds = _context.Reviews
                .GroupBy(r => r.BookId)
                .OrderByDescending(g => g.Average(r => r.Rating))
                .Select(g => g.Key)
                .Take(20); // optional: top 10 books

            return _context.Books
                .Where(b => topBookIds.Contains(b.Id))
                .Include(b => b.BookImages)
                    .ThenInclude(bi => bi.Image)
                .AsNoTracking();
        }


        public IQueryable<Book> GetTopSellingBooks()
        {
            var topBookIds = _context.OrderItems
                                 .GroupBy(oi => oi.BookId)
                                 .OrderByDescending(g => g.Count())
                                 .Select(g => g.Key);

            return _context.Books
                .Where(b => topBookIds.Contains(b.Id))
                .Include(b => b.Category).
                 Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                .AsNoTracking().AsQueryable();
        }

        public IQueryable<Book> GetSimilarBooks(Guid bookId)
        {
            var book = _context.Books
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == bookId);

            if (book == null)
                return Enumerable.Empty<Book>().AsQueryable();

            return _context.Books
                .Where(b => b.CategoryId == book.CategoryId && b.Id != bookId)
                .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                .OrderByDescending(b => b.ViewCount)
                .Include(b => b.Category)
                .AsQueryable()
                .AsNoTracking();
        }

        public IQueryable<Book> GetLowStockBooks(int threshold = 5)
        {
            return _context.Books
                .Where(b => b.Stock > 0 && b.Stock <= threshold)
                .OrderBy(b => b.Stock)
                .Include(b => b.Category)
                .AsQueryable()
                .AsNoTracking();
        }

        public async Task<IQueryable<Book>> SearchBooksQueryable<TKey>(
           Expression<Func<Book, TKey>> orderBy,
           Expression<Func<Book, bool>> searchPredicate = null,
           bool ascending = true)
        {
            IQueryable<Book> query = _context.Books
                .AsQueryable()
                .Include(b => b.Category)
                .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                .AsNoTracking();

            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }

            query = ascending ?
                query.OrderBy(orderBy) :
                query.OrderByDescending(orderBy);

            return await Task.FromResult(query);
        }

        public async Task<bool> IncrementBookViewCountAsync(Guid bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return false;

            book.ViewCount++;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalBooksCountAsync()
        {
            return await _context.Books.CountAsync();
        }

        public async Task<decimal> GetAverageBookRatingAsync(Guid bookId)
        {
            return (decimal)await _context.Reviews
                .Where(r => r.BookId == bookId)
                .AverageAsync(r => r.Rating);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAndCategoryAsync(Guid authorId, Guid categoryId)
        {
            return await _context.BookAuthors.Where(ba=>ba.AuthorId == authorId)
                .Join(_context.Books, ba=>ba.BookId , book=>book.Id,(bookAuthor, book) => book )
                .Where(book => book.CategoryId == categoryId)
                .Include(ub => ub.BookImages).ThenInclude(BI => BI.Image)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Book?> GetByIdAsync(Guid bookId, bool tracked = false)
        {
            return  tracked ?  await _context.Books.Include(b => b.Category).Include(b=>b.BookImages).ThenInclude(bi=>bi.Image).FirstOrDefaultAsync(b => b.Id == bookId):
                               await _context.Books.Include(b => b.Category).Include(b => b.BookImages).ThenInclude(bi => bi.Image).AsNoTracking().FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public new IQueryable<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookImages)
                .ThenInclude(bi=>bi.Image)
                .AsNoTracking()
                .AsQueryable();

        }

        public async Task<bool> IncreaseBookStockAmount(Book book, int amount)
        {
            book.Stock += amount;
            await  UpdateAsync(book);
            return true;
        }

        public async Task<bool> DecreaseBookStockAmount(Book book, int amount)
        {
            book.Stock -= amount;
            await UpdateAsync(book);
            return true;
        }
    }
}