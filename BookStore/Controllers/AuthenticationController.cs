using System;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Domain.Constants;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Helpers;
using BookStore.Infrastructure.Services.Interfaces;
using BookStore.WebAPI.ViewModels;
using BookStore.WebAPI.ViewModels.DetailedViewModels;
using BookStore.WebAPI.ViewModels.SimplifiedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService,
            IMapper mapper, ILogger<AuthenticationController> logger)
        {
            this.authenticationService = authenticationService;
            this.userService = userService;
            this.mapper = mapper;
            this.logger = logger;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterViewModel userRegisterViewModel)
        {
            var user = mapper.Map<User>(userRegisterViewModel);
            user.Role = UserRoles.UserRole;

            await userService.CreateAsync(user);

            logger.LogInformation($"Registered new user. Id: {user.Id}");

            return Ok(mapper.Map<UserDetailedViewModel>(user));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] UserLogintViewModel userLoginViewModel)
        {
            var user = await userService.GetUserByCredentialsAsync(userLoginViewModel.Email, userLoginViewModel.Password);

            if (user == null)
            {
                var message = "Invalid user`s login or password.";

                logger.LogInformation(message);
                return BadRequest(message);
            }

            var accessToken = await authenticationService.GetAccessTokenAsync(user);

            logger.LogInformation($"User log in. Id: {user.Id}");

            return Ok(new
            {
                User = mapper.Map<UserDetailedViewModel>(user),
                AccessToken = accessToken
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshUserTokenAsync()
        {
            var refreshToken = User.FindFirst(x => x.Type == AuthHelper.RefreshToken)?.Value;

            if (refreshToken == null)
            {
                var message = "There is no refresh token for specified user.";
                logger.LogInformation(message);

                return BadRequest(message);
            }

            var userId = AuthHelper.GetUserId(User);
            var user = await userService.GetByIdAsync(userId);

            if (user == null)
            {
                var message = "Specified user is not registered.";
                logger.LogInformation(message);

                return BadRequest(message);
            }

            var accessToken = await authenticationService.RefreshUserTokenAsync(userId, refreshToken);

            return Ok(new
            {
                User = mapper.Map<UserDetailedViewModel>(user),
                AccessToken = accessToken
            });
        }
    }
}