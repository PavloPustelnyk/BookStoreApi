﻿using System;
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

namespace BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.AdminRole)]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IBookCategoryService categoryService;
        private readonly IMapper mapper;
        private readonly ILogger<BooksController> logger;

        public BooksController(IBookService bookService, IAuthorService authorService, 
            IBookCategoryService bookCategoryService, IMapper mapper, ILogger<BooksController> logger)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.categoryService = bookCategoryService;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BookDetailedViewModel>>> GetBooks()
        {
            var books = await bookService.GetAll()
                                         .Include(b => b.Author)
                                         .Include(b => b.Reviews)
                                         .Include(b => b.LikedBy)
                                         .ToListAsync();

            return Ok(mapper.Map<IEnumerable<BookDetailedViewModel>>(books));
        }

        // GET: api/Books/page/5
        [HttpGet("page/{pageNo}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookPage(int pageNo)
        {
            var books = await bookService.GetAll()
                                         .Skip((pageNo - 1) * ControllersConstants.ItemsOnPageCount)
                                         .Take(ControllersConstants.ItemsOnPageCount)
                                         .Include(b => b.Author)
                                         .Include(b => b.Reviews)
                                         .Include(b => b.LikedBy)
                                         .ToListAsync();

            if (books == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ICollection<BookDetailedViewModel>>(books));
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookDetailedViewModel>> GetBook(int id)
        {
            var book = await bookService.GetAll()
                                        .Where(b => b.Id == id)
                                        .Include(b => b.Author)
                                        .Include(b => b.Reviews)
                                        .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            var bookViewModel = mapper.Map<BookDetailedViewModel>(book);
            var categories = await categoryService.GetAll()
                                                  .Where(c => c.BookId == id)
                                                  .Include(c => c.Category)
                                                  .Select(c => c.Category)
                                                  .ToListAsync();
            var bookLikesCount = bookService.GetBookLikesCountAsync(id);

            bookViewModel.Categories = mapper.Map<ICollection<CategoryViewModel>>(categories);

            return Ok(bookViewModel);
        }

        // GET: api/Books/5/Categories
        [HttpGet("{id}/categories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookGategories(int id)
        {
            var categories = await categoryService.GetAll()
                                                  .Where(c => c.BookId == id)
                                                  .Include(c => c.Category)
                                                  .Select(c => c.Category)
                                                  .ToListAsync();

            if (categories == null || categories.Count == 0)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] BookViewModel bookViewModel)
        {
            if (id != bookViewModel.Id)
            {
                return BadRequest();
            }

            try
            {
                var book = mapper.Map<Book>(bookViewModel);

                await bookService.UpdateAsync(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
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

            await bookService.CreateAsync(book);

            return Ok(mapper.Map<BookDetailedViewModel>(book));
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookDetailedViewModel>> DeleteBook(int id)
        {
            var book = await bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await bookService.DeleteAsync(book);

            return mapper.Map<BookDetailedViewModel>(book);
        }



        private bool BookExists(int id)
        {
            return bookService.GetAll().Any(b => b.Id == id);
        }
    }
}
