using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Domain.Entities
{
    public class BookCategory : BaseEntity
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Book Book { get; set; }

        public Category Category { get; set; }
    }
}
