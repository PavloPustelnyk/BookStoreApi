using BookStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services.Interfaces
{
    public interface IBookService : ICrudService<Book>
    {
        Task<int> GetBookLikesCountAsync(int id);

        Task UpdateBookWithCategoriesAsync(Book book, int[] categoriesId);

        Task CreateBookWithCategoriesAsync(Book book, int[] categoriesId);

        Task<ICollection<Book>> GetBooksByPartialTitleAsync(string partialTitle);

        Task<bool> ReviewExistsAsync(BookReview review);

        Task<BookReview> CreateBookReviewAsync(BookReview review);

        Task<ICollection<BookReview>> GetBookReviewsAsync(int bookId);

        Task<BookReview> DeleteBookReviewAsync(int reviewId, int userId);
    }
}
