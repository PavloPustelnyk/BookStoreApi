using BookStore.Domain.Entities;
using BookStore.Infrastructure.ApiContext;
using BookStore.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Infrastructure.Services
{
    public class BookCategoryService : CrudService<BookCategory, BookStoreDbContext>, IBookCategoryService
    {
        public BookCategoryService(BookStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
