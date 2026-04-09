using System.ComponentModel.DataAnnotations;

namespace Sepideh1.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Author { get; set; } = string.Empty;

        [StringLength(20)]
        public string? ISBN { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? PublishedDate { get; set; }

        [Range(0, int.MaxValue)]
        public int Pages { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
    }
}
