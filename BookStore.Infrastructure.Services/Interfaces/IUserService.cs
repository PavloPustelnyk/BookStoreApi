using BookStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services.Interfaces
{
    public interface IUserService : ICrudService<User>
    {
        Task<User> GetUserByCredentialsAsync(string email, string password);

        Task AddBookToFavoritesAsync(FavoriteBook book);

        Task DeleteBookFromFavoritesAsync(FavoriteBook book);

        Task<ICollection<FavoriteBook>> GetUserFavoriteBooksAsync(int userId);
    }
}
