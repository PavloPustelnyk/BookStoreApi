using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.ViewModels.SimplifiedViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        [Required]
        public string BookCategory { get; set; }
    }
}
