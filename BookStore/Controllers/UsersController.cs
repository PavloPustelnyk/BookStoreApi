using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Domain.Constants;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Services.Interfaces;
using BookStore.WebAPI.ViewModels.DetailedViewModels;
using BookStore.WebAPI.ViewModels.SimplifiedViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.AllUsersRole)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet("info")]
        public async Task<ActionResult<UserDetailedViewModel>> GetUserInfo()
        {
            var userViewModel = await GetCurrentUserAsync();

            if (userViewModel == null)
            {
                return NotFound();
            }

            return Ok(userViewModel);
        }

        [HttpPost("like-book")]
        public async Task<ActionResult<UserDetailedViewModel>> LikeBook(BaseViewModel bookId)
        {
            var userId = GetCurrentUserId();
            var favoriteBook = new FavoriteBook
            {
                UserId = userId,
                BookId = bookId.Id
            };

            await userService.AddBookToFavoritesAsync(favoriteBook);

            var userViewModel = await GetCurrentUserAsync();

            if (userViewModel == null)
            {
                return NotFound();
            }

            return Ok(userViewModel);
        }

        [HttpPost("remove-book")]
        public async Task<ActionResult<UserDetailedViewModel>> RemoveBook(BaseViewModel bookId)
        {
            var userId = GetCurrentUserId();
            var favoriteBook = new FavoriteBook
            {
                UserId = userId,
                BookId = bookId.Id
            };

            await userService.DeleteBookFromFavoritesAsync(favoriteBook);

            var userViewModel = await GetCurrentUserAsync();

            if (userViewModel == null)
            {
                return NotFound();
            }

            return Ok(userViewModel);
        }

        private async Task<UserDetailedViewModel> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();

            var user = await userService.GetByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var userViewModel = mapper.Map<UserDetailedViewModel>(user);
            var favoriteBooks = await userService.GetUserFavoriteBooksAsync(userId);
            userViewModel.LikedBooks = mapper.Map<ICollection<BookViewModel>>(favoriteBooks.Select(fb => fb.Book).ToArray());

            return userViewModel;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}