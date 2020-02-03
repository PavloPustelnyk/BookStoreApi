using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
