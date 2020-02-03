using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace BookStore.Infrastructure.Helpers
{
    public static class AuthHelper
    {
        public static SecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        public static int GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            int.TryParse(claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int id);
            return id;
        }

        public static string GetUserRefreshToken(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(x => x.Type == RefreshToken)?.Value;
        }

        public const string RefreshToken = "UserRefreshToken";
    }
}
