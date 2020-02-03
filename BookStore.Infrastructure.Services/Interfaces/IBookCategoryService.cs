using BookStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services.Interfaces
{
    public interface IBookCategoryService : ICrudService<BookCategory>
    {
        Task<ICollection<Category>> GetCategoriesAsync(int bookId);
    }
}
