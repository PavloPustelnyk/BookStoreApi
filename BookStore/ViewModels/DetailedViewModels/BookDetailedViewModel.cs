using BookStore.Domain.Entities;
using BookStore.WebAPI.ViewModels.SimplifiedViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.ViewModels.DetailedViewModels
{
    public class BookDetailedViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public decimal Price { get; set; }

        public IFormFile BookImage { get; set; }

        public string Description { get; set; }

        public AuthorViewModel Author { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }

        public ICollection<BookReviewViewModel> Reviews { get; set; }
    }
}
