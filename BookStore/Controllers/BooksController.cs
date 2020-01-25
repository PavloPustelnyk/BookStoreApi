using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.ApiContext;
using BookStore.Infrastructure.Services.Interfaces;
using AutoMapper;
using BookStore.WebAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BookStore.Domain.Constants;
using BookStore.WebAPI.Constants;
using Microsoft.Extensions.Logging;
using BookStore.WebAPI.ViewModels.DetailedViewModels;
using BookStore.WebAPI.ViewModels.SimplifiedViewModels;
using BookStore.Infrastructure.Helpers;

namespace BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.AdminRole)]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IBookCategoryService bookCategoryService;
        private readonly IMapper mapper;
        private readonly ILogger<BooksController> logger;

        public BooksController(IBookService bookService, IAuthorService authorService,
            IBookCategoryService bookCategoryService, IMapper mapper, ILogger<BooksController> logger)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.bookCategoryService = bookCategoryService;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Books/page/5
        [HttpGet("page/{pageNo}")]
        [ResponseCache(Location = ResponseCacheLocation.Any,
            VaryByQueryKeys = new[] { "pageNo" },
            Duration = ControllersConstants.CommonResponseCachingDuration)]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookPage(int pageNo)
        {
            if (pageNo < 1)
            {
                return BadRequest("Wrong page number.");
            }

            var books = await bookService.GetAll()
                .Skip((pageNo - 1) * ControllersConstants.ItemsOnPageCount)
                .Take(ControllersConstants.ItemsOnPageCount)
                .Include(b => b.Author)
                .ToListAsync();

            if (books == null)
            {
                return NotFound($"Page with number '{pageNo}' is empty.");
            }

            var bookViewModels = mapper.Map<ICollection<BookDetailedViewModel>>(books);

            foreach (var bookViewModel in bookViewModels)
            {
                var categories = await bookCategoryService.GetCategoriesAsync(bookViewModel.Id);
                bookViewModel.Categories = mapper.Map<ICollection<CategoryViewModel>>(categories);
            }

            return Ok(bookViewModels);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.Any,
            VaryByQueryKeys = new[] { "id" },
            Duration = ControllersConstants.CommonResponseCachingDuration)]
        [AllowAnonymous]
        public async Task<ActionResult<BookDetailedViewModel>> GetBook(int id)
        {
            var book = await bookService.GetAll()
                .Where(b => b.Id == id)
                .Include(b => b.Author)
                .Include(b => b.Reviews)
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound($"Book with id '{id}' does not exist.");
            }

            var bookViewModel = mapper.Map<BookDetailedViewModel>(book);

            bookViewModel.Categories = mapper.Map<ICollection<CategoryViewModel>>(book.BookCategories
                .Select(bc => bc.Category)
                .ToList());

            return Ok(bookViewModel);
        }

        // GET: api/Books/search/str
        [HttpGet("search/{partialTitle}")]
        [ResponseCache(Location = ResponseCacheLocation.Any,
            VaryByQueryKeys = new[] { "partialTitle" },
            Duration = ControllersConstants.CommonResponseCachingDuration)]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<BookDetailedViewModel>>> GetBookByPartialTitle(string partialTitle)
        {
            var books = await bookService.GetBooksByPartialTitleAsync(partialTitle);

            var bookViewModels = mapper.Map<ICollection<BookDetailedViewModel>>(books);

            foreach (var bookViewModel in bookViewModels)
            {
                var categories = await bookCategoryService.GetCategoriesAsync(bookViewModel.Id);
                bookViewModel.Categories = mapper.Map<ICollection<CategoryViewModel>>(categories);
            }

            return Ok(bookViewModels);
        }

        // GET: api/Books/5/Categories
        [HttpGet("{id}/categories")]
        [ResponseCache(Location = ResponseCacheLocation.Any,
            VaryByQueryKeys = new[] { "id" },
            Duration = ControllersConstants.CommonResponseCachingDuration)]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookGategories(int id)
        {
            var categories = await bookCategoryService.GetAll()
                .Where(c => c.BookId == id)
                .Include(c => c.Category)
                .Select(c => c.Category)
                .ToListAsync();

            if (categories == null)
            {
                return NotFound($"There are no categories for specified book id: {id}.");
            }

            return Ok(categories);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] BookViewModel bookViewModel)
        {
            if (id != bookViewModel.Id)
            {
                return BadRequest($"Wrong book id: {id}");
            }

            if (!BookExists(id))
            {
                return NotFound($"Book with id '{id}' does not exist.");
            }

            try
            {
                var book = mapper.Map<Book>(bookViewModel);

                await bookService.UpdateBookWithCategoriesAsync(book, bookViewModel.CategoriesId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound($"Book with id '{id}' does not exist.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDetailedViewModel>> PostBook([FromBody] BookViewModel bookViewModel)
        {
            var book = mapper.Map<Book>(bookViewModel);

            await bookService.CreateBookWithCategoriesAsync(book, bookViewModel.CategoriesId);

            return Ok(mapper.Map<BookDetailedViewModel>(book));
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookDetailedViewModel>> DeleteBook(int id)
        {
            var book = await bookService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with id '{id}' does not exist.");
            }

            await bookService.DeleteAsync(book);

            return mapper.Map<BookDetailedViewModel>(book);
        }

        // GET: api/Books/{id}/reviews
        [HttpGet("{id}/reviews")]
        [ResponseCache(Location = ResponseCacheLocation.Any,
            VaryByQueryKeys = new[] { "id" },
            Duration = ControllersConstants.CommonResponseCachingDuration)]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<BookReviewViewModel>>> GetBookReviews(int id)
        {
            if (!BookExists(id))
            {
                return NotFound($"Book with id '{id}' does not exist.");
            }

            var reviews = await bookService.GetBookReviewsAsync(id);

            return Ok(mapper.Map<ICollection<BookReviewViewModel>>(reviews));
        }

        // POST: api/Books/reviews
        [HttpPost("reviews")]
        [Authorize(Roles = UserRoles.AllUsersRole)]
        public async Task<ActionResult<BookReviewViewModel>> PostReview([FromBody] BookReviewViewModel bookReviewViewModel)
        {
            if (!BookExists(bookReviewViewModel.BookId))
            {
                return NotFound($"Book with id '{bookReviewViewModel.BookId}' does not exist.");
            }

            var bookReview = mapper.Map<BookReview>(bookReviewViewModel);
            bookReview.UserId = AuthHelper.GetUserId(User);

            await bookService.CreateBookReviewAsync(bookReview);

            return Ok(mapper.Map<BookReviewViewModel>(bookReview));
        }

        // DELETE: api/Books/reviews
        [HttpDelete("reviews")]
        [Authorize(Roles = UserRoles.AllUsersRole)]
        public async Task<ActionResult<BookReviewViewModel>> DeleteReview(int id)
        {
            var bookReview = await bookService.DeleteBookReviewAsync(id, AuthHelper.GetUserId(User));

            if (bookReview == null)
            {
                return BadRequest($"Review with id '{id}' does not exist for authorized user.");
            }

            return mapper.Map<BookReviewViewModel>(bookReview);
        }

        private bool BookExists(int id)
        {
            return bookService.GetAll().Any(b => b.Id == id);
        }
    }
}
