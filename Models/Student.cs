using System.ComponentModel.DataAnnotations;

namespace Sepideh1.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty; //todo: test if we remove string empty what will be happen

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
    }
}
