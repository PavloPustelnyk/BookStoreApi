using System.ComponentModel.DataAnnotations;

namespace BookStore.WebAPI.ViewModels.SimplifiedViewModels
{
    public class BookViewModel : BaseViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int[] CategoriesId { get; set; } 

        public string BookImage { get; set; }

        public string Description { get; set; }
    }
}
