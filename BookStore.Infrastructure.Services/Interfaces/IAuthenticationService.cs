using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetAccessTokenAsync(User user);
        Task<string> RefreshUserTokenAsync(int userId, string oldRefreshToken);
    }
}
