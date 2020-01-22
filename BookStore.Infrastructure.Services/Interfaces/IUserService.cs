using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services.Interfaces
{
    public interface IUserService : ICrudService<User>
    {
        Task<User> GetUserByCredentialsAsync(string email, string password);

        Task AddBookToFavorites(FavoriteBook book);
    }
}
