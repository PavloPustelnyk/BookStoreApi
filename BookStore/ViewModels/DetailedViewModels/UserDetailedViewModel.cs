﻿using BookStore.WebAPI.ViewModels.SimplifiedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.ViewModels.DetailedViewModels
{
    public class UserDetailedViewModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<BookViewModel> LikedBooks { get; set; }
    }
}
