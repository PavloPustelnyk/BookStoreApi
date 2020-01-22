using BookStore.Domain.Entities;
using BookStore.Infrastructure.ApiContext;
using BookStore.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services
{
    public class BookService : CrudService<Book, BookStoreDbContext>, IBookService
    {
        public BookService(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public Task AddBookReview(BookReview review)
        {
            throw new NotImplementedException();
        }
    }
}
