﻿using BookStore.Domain.Entities;
using BookStore.Infrastructure.ApiContext;
using BookStore.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoHelper;

namespace BookStore.Infrastructure.Services
{
    public class UserService : CrudService<User, BookStoreDbContext>, IUserService
    {
        public UserService(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(User entity)
        {
            entity.Password = Crypto.HashPassword(entity.Password);
            entity.RefreshToken = GenerateRefreshToken();

            return base.CreateAsync(entity);
        }

        public async Task<User> GetUserByCredentialsAsync(string email, string password)
        {
            var user = await GetAll().Where(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return Crypto.VerifyHashedPassword(user.Password, password) ? user : null;
        }



        private string GenerateRefreshToken()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return Base64Encode(new string(Enumerable.Repeat(chars, 25)
                .Select(s => s[random.Next(s.Length)]).ToArray()));
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            return Convert.ToBase64String(plainTextBytes);
        }

        public async Task AddBookToFavoritesAsync(FavoriteBook book)
        {
            await dbContext.Set<FavoriteBook>().AddAsync(book).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteBookFromFavoritesAsync(FavoriteBook book)
        {
            var entity = await dbContext.Set<FavoriteBook>()
                                        .Where(fb => fb.BookId == book.BookId && fb.UserId == book.UserId)
                                        .FirstOrDefaultAsync();

            if (entity != null)
            {
                dbContext.Set<FavoriteBook>().Remove(entity);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<ICollection<FavoriteBook>> GetUserFavoriteBooksAsync(int userId)
        {
            return await dbContext.Set<FavoriteBook>().Where(fb => fb.UserId == userId).Include(fb => fb.Book).ToListAsync();
        }
    }
}
