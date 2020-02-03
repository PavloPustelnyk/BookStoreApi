using BookStore.Domain.Entities;
using BookStore.Infrastructure.ApiContext;
using BookStore.Infrastructure.Services.Interfaces;

namespace BookStore.Infrastructure.Services
{
    public class BookReviewService : CrudService<BookReview, BookStoreDbContext>, IBookReviewService
    {
        public BookReviewService(BookStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
