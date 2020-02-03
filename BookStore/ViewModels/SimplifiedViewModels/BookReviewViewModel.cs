using System.ComponentModel.DataAnnotations;

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
