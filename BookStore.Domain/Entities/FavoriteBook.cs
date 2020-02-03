using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public class FavoriteBook : BaseEntity
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        public Book Book { get; set; }

        public User User { get; set; }
    }
}
