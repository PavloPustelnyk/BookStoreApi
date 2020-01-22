using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Infrastructure.Services.Interfaces
{
    public interface ICategoryService: ICrudService<Category>
    {
    }
}
