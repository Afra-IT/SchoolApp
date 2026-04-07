using System.ComponentModel.DataAnnotations;

namespace Sepideh1.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
