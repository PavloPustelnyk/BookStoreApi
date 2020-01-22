using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Domain.Constants;
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
    [Authorize(Roles = UserRoles.AdminRole)]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(IAuthorService authorService, IMapper mapper, ILogger<AuthorsController> logger)
        {
            this.authorService = authorService;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Author
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> GetAuthors()
        {
            var authors = await authorService.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<AuthorViewModel>>(authors));
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthorViewModel>> GetAuthor(int id)
        {
            var author = await authorService.GetByIdAsync(id);

            if (author == null)
            {
                logger.LogInformation($"Non-existing author id: '{id}'.");

                return NotFound();
            }

            return mapper.Map<AuthorViewModel>(author);
        }

        // GET: api/Authors/5/Books
        [HttpGet("{id}/books")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthorBooks(int id)
        {
            var author = await authorService.GetAll().Where(a => a.Id == id).Include(a => a.Books).FirstOrDefaultAsync();

            if (author == null)
            {
                logger.LogInformation($"Non-existing author id: '{id}'.");

                return NotFound();
            }

            return Ok(author.Books);
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, [FromBody] AuthorViewModel authorViewModel)
        {
            if (id != authorViewModel.Id)
            {
                logger.LogInformation($"Wrong specified author id: '{id}'.");

                return BadRequest();
            }

            try
            {
                var author = mapper.Map<Author>(authorViewModel);

                await authorService.UpdateAsync(author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult<AuthorViewModel>> PostAuthor([FromBody] AuthorViewModel authorViewModel)
        {
            var author = mapper.Map<Author>(authorViewModel);

            await authorService.CreateAsync(author);

            return Ok(mapper.Map<AuthorViewModel>(author));
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorViewModel>> DeleteAuthor(int id)
        {
            var author = await authorService.GetByIdAsync(id);
            if (author == null)
            {
                logger.LogInformation($"Non-existing author id: '{id}'.");

                return NotFound();
            }

            await authorService.DeleteAsync(author);

            return mapper.Map<AuthorViewModel>(author);
        }

        private bool AuthorExists(int id)
        {
            return authorService.GetAll().Any(a => a.Id == id);
        }
    }
}