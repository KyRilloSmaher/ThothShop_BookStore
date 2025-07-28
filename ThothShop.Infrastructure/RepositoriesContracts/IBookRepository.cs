using ThothShop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ThothShop.Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;
using ThothShop.Infrastructure.Implementations;
using System.Linq.Expressions;

namespace  ThothShop.Infrastructure.Contracts
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IQueryable<Book> GetBooksByCategory(Guid categoryId);
        Task<IQueryable<Book>> GetBooksByAuthorAsync(Guid authorId);
        Task<IEnumerable<Book>> GetBooksByAuthorOrderedByViewsAsync(Guid authorId);
        IQueryable<Book> GetBooksOrderedByStockAscendingAsync();
        IQueryable<Book> GetBooksOrderedByStockDescendingAsync();
        Task<IEnumerable<Book>> GetOutOfStockBooksAsync();
        Task<bool> IncrementBookViewCountAsync(Guid bookId);
        Task<int> GetTotalBooksCountAsync();
        Task<decimal> GetAverageBookRatingAsync(Guid bookId);
        Task<IEnumerable<Book>> GetBooksByAuthorAndCategoryAsync(Guid authorId, Guid categoryId);
        public IQueryable<Book> GetNewReleases();
        new IQueryable<Book> GetAll();
        public IQueryable<Book> GetTopRatedBooks();
        public IQueryable<Book> GetTopSellingBooks();
        public IQueryable<Book> GetSimilarBooks(Guid bookId);
        public IQueryable<Book> GetLowStockBooks(int threshold = 5);
        public Task<IQueryable<Book>> SearchBooksQueryable<TKey>(
          Expression<Func<Book, TKey>> orderBy,
          Expression<Func<Book, bool>> searchPredicate = null,
          bool ascending = true);
        Task <bool> IncreaseBookStockAmount(Book book , int amount);
        Task <bool> DecreaseBookStockAmount(Book book , int amount);
    }
}