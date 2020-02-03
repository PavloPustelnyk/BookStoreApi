using System.ComponentModel.DataAnnotations;

namespace BookStore.WebAPI.ViewModels.SimplifiedViewModels
{
    public class UserLogintViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
