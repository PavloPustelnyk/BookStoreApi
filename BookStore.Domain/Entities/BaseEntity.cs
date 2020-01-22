using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
