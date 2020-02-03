using System.ComponentModel.DataAnnotations;

namespace BookStore.WebAPI.ViewModels.SimplifiedViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        [Required]
        public string BookCategory { get; set; }
    }
}
