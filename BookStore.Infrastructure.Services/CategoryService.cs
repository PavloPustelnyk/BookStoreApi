﻿using BookStore.Domain.Entities;
using BookStore.Infrastructure.ApiContext;
using BookStore.Infrastructure.Services.Interfaces;

namespace BookStore.Infrastructure.Services
{
    public class CategoryService : CrudService<Category, BookStoreDbContext>, ICategoryService
    {
        public CategoryService(BookStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
