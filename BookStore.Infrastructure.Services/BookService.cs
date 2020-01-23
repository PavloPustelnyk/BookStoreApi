using BookStore.Domain.Entities;
using BookStore.Infrastructure.ApiContext;
using BookStore.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services
{
    public class BookService : CrudService<Book, BookStoreDbContext>, IBookService
    {
        public BookService(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateBookWithCategoriesAsync(Book book, int[] categoriesId)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                await dbContext.Books.AddAsync(book);
                await dbContext.SaveChangesAsync();

                if (categoriesId != null)
                {
                    AddBookCategories(book.Id, categoriesId);
                    await dbContext.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
        }

        public async Task<int> GetBookLikesCountAsync(int id)
        {
            var book = await GetAll().Where(b => b.Id == id).Include(b => b.LikedBy).FirstOrDefaultAsync();

            if (book == null)
            {
                return 0;
            }

            return book.LikedBy.Count;
        }

        public async Task<ICollection<Book>> GetBooksByPartialTitleAsync(string partialTitle)
        {
            return await dbContext.Books
                .Where(b => EF.Functions.Like(b.Title, $"%{partialTitle}%"))
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task UpdateBookWithCategoriesAsync(Book book, int[] categoriesId)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                if (categoriesId != null)
                {
                    var oldCategories = await dbContext.BookCategories.Where(bc => bc.BookId == book.Id).ToArrayAsync();
                    dbContext.BookCategories.RemoveRange(oldCategories);

                    AddBookCategories(book.Id, categoriesId);
                }

                await UpdateAsync(book);

                await transaction.CommitAsync();
            }
        }

        private void AddBookCategories(int bookId, int[] categoriesId)
        {
            var categories = categoriesId.Select(i => new BookCategory { BookId = bookId, CategoryId = i });
            dbContext.Set<BookCategory>().AddRange(categories);
        }
    }
}
