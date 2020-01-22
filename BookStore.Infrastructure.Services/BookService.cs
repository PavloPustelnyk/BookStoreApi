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

        public async Task<int> GetBookLikesCountAsync(int id)
        {
            var book = await GetAll().Where(b => b.Id == id).Include(b => b.LikedBy).FirstOrDefaultAsync();

            if (book == null)
            {
                return 0;
            }

            return book.LikedBy.Count;
        }
    }
}
