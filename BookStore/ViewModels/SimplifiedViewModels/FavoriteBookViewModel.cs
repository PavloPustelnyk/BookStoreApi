using System.ComponentModel.DataAnnotations;

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
