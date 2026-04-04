using System.ComponentModel.DataAnnotations;

namespace Sepideh1.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }
    }
}
