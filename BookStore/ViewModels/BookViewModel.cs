using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile BookImage { get; set; }

        public string Description { get; set; }

        public AuthorViewModel Author { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }

        public ICollection<BookReviewViewModel> Reviews { get; set; }

        public ICollection<FavoriteBookViewModel> LikedBy { get; set; }
    }
}
