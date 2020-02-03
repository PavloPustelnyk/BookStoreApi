using BookStore.Domain.Entities;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetAccessTokenAsync(User user);
        Task<string> RefreshUserTokenAsync(int userId, string oldRefreshToken);
    }
}
