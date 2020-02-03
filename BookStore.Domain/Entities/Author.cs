using BookStore.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class Author : BaseEntity
    {
        [Required]
        [StringLength(DbColumnConstraints.FirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(DbColumnConstraints.LastNameLength)]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }

        [MaxLength(DbColumnConstraints.ImageSize)]
        public string AuthorImage { get; set; }

        [StringLength(DbColumnConstraints.DescriptionLength)]
        public string Description { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
