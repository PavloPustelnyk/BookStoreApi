using BookStore.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(DbColumnConstraints.EmailLength)]
        public string Email { get; set; }
    
        [Required]
        [StringLength(DbColumnConstraints.PasswordLength)]
        public string Password { get; set; }

        [Required]
        [StringLength(DbColumnConstraints.RoleLength)]
        public string Role { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        [Required]
        [StringLength(DbColumnConstraints.FirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(DbColumnConstraints.LastNameLength)]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }

        public ICollection<BookReview> Reviews { get; set; }

        public ICollection<FavoriteBook> FavoriteBooks { get; set; }
    }
}