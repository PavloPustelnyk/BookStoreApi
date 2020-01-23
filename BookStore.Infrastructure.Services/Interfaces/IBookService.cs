using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services.Interfaces
{
    public interface IBookService : ICrudService<Book>
    {
        Task<int> GetBookLikesCountAsync(int id);

        Task UpdateBookWithCategoriesAsync(Book book, int[] categoriesId);

        Task CreateBookWithCategoriesAsync(Book book, int[] categoriesId);

        Task<ICollection<Book>> GetBooksByPartialTitleAsync(string partialTitle);
    }
}
