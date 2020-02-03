using BookStore.WebAPI.ViewModels.SimplifiedViewModels;
using System;
using System.Collections.Generic;

namespace BookStore.WebAPI.ViewModels.DetailedViewModels
{
    public class AuthorDetailedViewModel : BaseViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string AuthorImage { get; set; }

        public string Description { get; set; }

        public ICollection<BookViewModel> Books { get; set; }
    }
}
