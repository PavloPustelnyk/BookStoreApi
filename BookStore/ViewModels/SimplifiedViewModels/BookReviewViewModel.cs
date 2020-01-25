using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.ViewModels.SimplifiedViewModels
{
    public class BookReviewViewModel : BaseViewModel
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
