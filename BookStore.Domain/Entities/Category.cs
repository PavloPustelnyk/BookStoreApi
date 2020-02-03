using BookStore.Domain.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(DbColumnConstraints.BookCategoryLength)]
        public string BookCategory { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
