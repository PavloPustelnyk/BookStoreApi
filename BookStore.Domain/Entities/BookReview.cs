using BookStore.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public class BookReview : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }

        [StringLength(DbColumnConstraints.BookCommentLength)]
        public string Comment { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }
    }
}
