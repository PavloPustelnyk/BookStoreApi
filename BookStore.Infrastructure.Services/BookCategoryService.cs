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
    public class BookCategoryService : CrudService<BookCategory, BookStoreDbContext>, IBookCategoryService
    {
        public BookCategoryService(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<Category>> GetCategoriesAsync(int bookId)
        {
            return await dbContext.BookCategories
                .Where(bc => bc.BookId == bookId)
                .Include(bc => bc.Category)
                .Select(bc => bc.Category)
                .ToListAsync();
        }
    }
}
