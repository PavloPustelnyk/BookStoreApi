using BookStore.Domain.Entities;
using BookStore.Infrastructure.ApiContext;
using BookStore.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services
{
    public class AuthorService : CrudService<Author, BookStoreDbContext>, IAuthorService
    {
        public AuthorService(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Author> GetAuthorWithBooks(int id)
        {
            return await dbContext.Authors
                .Include(a => a.Books)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Author>> GetAuthorsByPartialNameAsync(string partialName)
        {
            return await dbContext.Authors
                .Where(a => EF.Functions.Like(a.FirstName + " " + a.LastName, $"%{partialName}%"))
                .ToListAsync();
        }
    }
}
