using BookStore.Domain.Entities;
using BookStore.Infrastructure.ApiContext;
using BookStore.Infrastructure.Services;
using BookStore.Tests.FakeRepositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Tests.ServicesTests
{
    public class BookServiceTest
    {
        private BookStoreDbContext dbContext;
        private DbContextOptions<BookStoreDbContext> options;
        private BookService bookService;
        private InMemoryDb inMemoryDb;

        public BookServiceTest()
        {
            options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreDb")
                .Options;

            InitializeDb(options);

            bookService = new BookService(dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GetBookPageTest()
        {
            var expectedCount = inMemoryDb.Books.Count;

            var books = bookService.GetAll();

            Assert.IsNotNull(books);
            Assert.AreEqual(expectedCount, books.Count());
        }

        [Test]
        public async Task GetBookTest()
        {
            int id = 1;

            var book = await bookService.GetByIdAsync(id);

            Assert.IsNotNull(book);
        }

        [Test]
        public async Task AddBookTest()
        {
            var book = new Book
            {
                Title = "Test Title",
                AuthorId = 1,
                Price = 100,
                ReviewCount = 10,
                SummaryRating = 50
            };

            var oldCount = bookService.GetAll().Count();
            await bookService.CreateAsync(book);

            Assert.IsTrue(bookService.GetAll().Count() > oldCount);
            await bookService.DeleteAsync(book);
        }

        [Test]
        public async Task UpdateBookTest()
        {
            int id = 1;
            string newTitle = "New title";
            var book = await bookService.GetByIdAsync(id);
            book.Title = newTitle;

            await bookService.UpdateAsync(book);

            Assert.AreEqual(newTitle, book.Title);
        }

        [Test]
        public async Task DeleteBookTest()
        {
            int id = 2;
            var book = await bookService.GetByIdAsync(id);

            await bookService.DeleteAsync(book);
            var deletedBook = await bookService.GetByIdAsync(id);

            Assert.IsNull(deletedBook);
            await bookService.CreateAsync(book);
        }

        private void InitializeDb(DbContextOptions<BookStoreDbContext> options)
        {
            inMemoryDb = new InMemoryDb();
            dbContext = new BookStoreDbContext(options);

            foreach (var author in inMemoryDb.Authors)
            {
                dbContext.Authors.Add(author);
            }

            dbContext.SaveChanges();

            foreach (var book in inMemoryDb.Books)
            {
                dbContext.Books.Add(book);
            }

            dbContext.SaveChanges();

        }
    }
}
