using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BookStore.Tests.FakeRepositories
{
    public class InMemoryDb
    {
        public InMemoryDb()
        {
            Books = GetBooks();
            BookReviews = GetBookReviews();
            BookCategories = GetBookCategories();
            Categories = GetCategories();
            Users = GetUsers();
            Authors = GetAuthors();
        }

        public List<Book> Books { get; }
        public List<BookReview> BookReviews { get; }
        public List<BookCategory> BookCategories { get; }
        public List<Category> Categories { get; }
        public List<User> Users { get; }
        public List<Author> Authors { get; }
        public List<FavoriteBook> FavoriteBooks { get; }

        private List<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Test Title 1",
                    AuthorId = 1,
                    Price = 100,
                    ReviewCount = 10,
                    SummaryRating = 50,
                    BookImage = null,
                    Description = null
                },
                new Book
                {
                    Id = 2,
                    Title = "Test Title 2",
                    AuthorId = 1,
                    Price = 100,
                    ReviewCount = 10,
                    SummaryRating = 50,
                    BookImage = null,
                    Description = null
                },
                new Book
                {
                    Id = 3,
                    Title = "Test Title 3",
                    AuthorId = 1,
                    Price = 100,
                    ReviewCount = 10,
                    SummaryRating = 50,
                    BookImage = null,
                    Description = null
                },
            };
        }

        private List<BookReview> GetBookReviews()
        {
            return new List<BookReview>
            {
                new BookReview
                {
                    UserId = 1,
                    BookId = 1,
                    Rating = 5,
                    Comment = "Test comment"
                },
                new BookReview
                {
                    UserId = 1,
                    BookId = 2,
                    Rating = 5,
                    Comment = "Test comment"
                },
                new BookReview
                {
                    UserId = 1,
                    BookId = 3,
                    Rating = 5,
                    Comment = "Test comment"
                }
            };
        }

        private List<Author> GetAuthors()
        {
            return new List<Author>
            {
                new Author
                {
                    Id = 1,
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    BirthDate = new DateTime(1900, 01, 01),
                    AuthorImage = null,
                    Description = null
                },
                new Author
                {
                    Id = 2,
                    FirstName = "FirstName2",
                    LastName = "LastName2",
                    BirthDate = new DateTime(1900, 01, 01),
                    AuthorImage = null,
                    Description = null
                },
                new Author
                {
                    Id = 3,
                    FirstName = "FirstName3",
                    LastName = "LastName3",
                    BirthDate = new DateTime(1900, 01, 01),
                    AuthorImage = null,
                    Description = null
                }
            };
        }

        private List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "test@email.com",
                    Password = "test",
                    Role = "user",
                    RefreshToken = "refresh",
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    BirthDate = new DateTime(1900, 01, 01)
                },
                new User
                {
                    Id = 2,
                    Email = "test@email.com",
                    Password = "test",
                    Role = "user",
                    RefreshToken = "refresh",
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    BirthDate = new DateTime(1900, 01, 01)
                },
                new User
                {
                    Id = 3,
                    Email = "test@email.com",
                    Password = "test",
                    Role = "user",
                    RefreshToken = "refresh",
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    BirthDate = new DateTime(1900, 01, 01)
                }
            };
        }

        private List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    BookCategory = "category1"
                }
            };
        }

        private List<BookCategory> GetBookCategories()
        {
            return new List<BookCategory>
            {
                new BookCategory
                {
                    Id = 1,
                    BookId = 1,
                    CategoryId = 1
                },
                new BookCategory
                {
                    Id = 2,
                    BookId = 2,
                    CategoryId = 1
                },
                new BookCategory
                {
                    Id = 3,
                    BookId = 3,
                    CategoryId = 1
                }
            };
        }
    }
}
