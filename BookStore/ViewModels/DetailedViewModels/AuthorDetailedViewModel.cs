using BookStore.WebAPI.ViewModels.SimplifiedViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.ViewModels.DetailedViewModels
{
    public class AuthorDetailedViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public IFormFile AuthorImage { get; set; }

        public string Description { get; set; }

        public ICollection<BookViewModel> Books { get; set; }
    }
}
