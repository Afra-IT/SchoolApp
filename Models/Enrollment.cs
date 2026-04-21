using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sepideh1.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int LessonId { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }

        [ForeignKey(nameof(LessonId))]
        public Lesson? Lesson { get; set; }
    }
}
