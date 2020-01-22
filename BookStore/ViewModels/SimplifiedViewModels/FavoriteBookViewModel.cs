using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.ViewModels.SimplifiedViewModels
{
    public class FavoriteBookViewModel : BaseViewModel
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
