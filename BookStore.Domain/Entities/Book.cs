using BookStore.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStore.Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        [StringLength(DbColumnConstraints.BookTitleLength)]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal Price { get; set; }

        [Required]
        public int ReviewCount { get; set; }

        [Required]
        public int SummaryRating { get; set; }

        [MaxLength(DbColumnConstraints.ImageSize)]
        public byte[] BookImage { get; set; }

        [StringLength(DbColumnConstraints.DescriptionLength)]
        public string Description { get; set; }

        public Author Author { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }

        public ICollection<BookReview> Reviews { get; set; }

        public ICollection<FavoriteBook> LikedBy { get; set; }
    }
}
