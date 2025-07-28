using Microsoft.AspNetCore.Http;
using ThothShop.Domain.Models;
using ThothShop.Service.Commans;

namespace ThothShop.Service.Contract
{
    public interface IBookService
    {
        Task<Book?> GetBookByIdAsync(Guid id, bool tracked = false);
        Task<Book?> GetBookByIdIncludingCategoryAsNotrackingAsync(Guid id, bool tracked = false);
        Task<string> CreateBookAsync(Book book, IFormFile primaryImage, List<IFormFile> images);
        Task<bool> UpdateBookAsync(Book book ,IFormFile? primaryImage , IList<IFormFile>? Images );
        Task<bool> DeleteBookAsync(Book book);
        Task<decimal> GetAverageBookRatingAsync(Guid bookId);
        Task<bool> IsBookIdExits(Guid bookId);
        Task<bool> IncreaseBookStock(Guid bookid, int amount);
        Task<bool> DecreaseBookStock(Guid bookid, int amount);
        Task<int> GetTotalBooksCountAsync();
        Task<IEnumerable<Author>> GetAuthorsForBookAsync(Guid bookId);
        Task<IEnumerable<Book>> GetBooksByAuthorAndCategoryAsync(Guid authorId, Guid categoryId);
        Task<IQueryable<Book>> GetBooksByAuthorAsync(Guid authorId);
        Task<IEnumerable<Book>> GetBooksByAuthorOrderedByViewsAsync(Guid authorId);
        Task<IQueryable<Book>> GetBooksByCategoryAsync(Guid categoryId);
        Task<IQueryable<Book>> GetBooksOrderedByStockAscendingAsync();
        Task<IQueryable<Book>> GetBooksOrderedByStockDescendingAsync();
        Task<IEnumerable<Book>> GetOutOfStockBooksAsync();
        Task<IQueryable<Book>> GetAllBooksAsync();
        Task<IQueryable<Book>> SearchBooksQueryable(FilterListParams model);

        IQueryable<Book> GetNewReleases();
        IQueryable<Book> GetTopRatedBooks();
        IQueryable<Book> GetTopSellingBooks();
        IQueryable<Book> GetSimilarBooks(Guid bookId);
        IQueryable<Book> GetLowStockBooks(int threshold = 5);
    }
}
