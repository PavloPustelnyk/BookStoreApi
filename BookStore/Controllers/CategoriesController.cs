﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Services.Interfaces;
using BookStore.WebAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IBookCategoryService bookCategoryService;
        private readonly IMapper mapper;
        private readonly ILogger<CategoriesController> logger;

        public CategoriesController(ICategoryService categoryService, IBookCategoryService bookCategoryService, 
            IMapper mapper, ILogger<CategoriesController> logger)
        {
            this.categoryService = categoryService;
            this.bookCategoryService = bookCategoryService;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Categories
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategories()
        {
            var categories = await categoryService.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryViewModel>> GetCategory(int id)
        {
            var category = await categoryService.GetByIdAsync(id);

            if (category == null)
            {
                logger.LogInformation($"Non-existing category id: '{id}'.");

                return NotFound();
            }

            return mapper.Map<CategoryViewModel>(category);
        }

        // GET: api/Categories/5/Books
        [HttpGet("{id}/books")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryBooks(int id)
        {
            var books = await bookCategoryService.GetAll()
                                                 .Where(bc => bc.CategoryId == id)
                                                 .Include(bc => bc.Book)
                                                 .Select(bc => bc.Book)
                                                 .ToListAsync();

            if (books == null || books.Count == 0)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                logger.LogInformation($"Wrong category id: '{id}'.");

                return BadRequest();
            }

            try
            {
                var author = mapper.Map<Category>(categoryViewModel);

                await categoryService.UpdateAsync(author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    logger.LogInformation($"Non-existing author id: '{id}'.");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> PostCategory([FromBody] CategoryViewModel categoryViewModel)
        {
            var category = mapper.Map<Category>(categoryViewModel);

            await categoryService.CreateAsync(category);

            return Ok(mapper.Map<AuthorViewModel>(category));
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryViewModel>> DeleteCategory(int id)
        {
            var category = await categoryService.GetByIdAsync(id);
            if (category == null)
            {
                logger.LogInformation($"Non-existing author id: '{id}'.");

                return NotFound();
            }

            await categoryService.DeleteAsync(category);

            return mapper.Map<CategoryViewModel>(category);
        }

        private bool CategoryExists(int id)
        {
            return categoryService.GetAll().Any(a => a.Id == id);
        }
    }
}