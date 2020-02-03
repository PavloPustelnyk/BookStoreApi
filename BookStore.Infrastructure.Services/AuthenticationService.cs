using BookStore.Domain.Entities;
using BookStore.Infrastructure.Helpers;
using BookStore.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public AuthenticationService(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        public async Task<string> GetAccessTokenAsync(User user)
        {
            var claimsIdentity = await GetClaimsIdentityAsync(user);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: configuration["AuthenticationOptions:Issuer"],
                audience: configuration["AuthenticationOptions:Audience"],
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(int.Parse(configuration["AuthenticationOptions:Lifetime"]))),
                signingCredentials: new SigningCredentials(AuthHelper.GetSymmetricSecurityKey(configuration["AuthenticationOptions:Key"]), 
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<string> RefreshUserTokenAsync(int userId, string oldRefreshToken)
        {
            var user = await userService.GetByIdAsync(userId);

            if (user.RefreshToken != oldRefreshToken)
            {
                return null;
            }

            return await GetAccessTokenAsync(user);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentityAsync(User userEntity)
        {
            var user = await userService.GetAll()
                                        .Where(u => u.Email == userEntity.Email && u.Password == userEntity.Password)
                                        .FirstOrDefaultAsync();

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(AuthHelper.RefreshToken, user.RefreshToken)
                };

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }
    }
}
