using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services.Interfaces
{
    public interface IAuthorService : ICrudService<Author>
    {
        Task<Author> GetAuthorWithBooks(int id);

        Task<ICollection<Author>> GetAuthorsByPartialNameAsync(string partialName);
    }
}
