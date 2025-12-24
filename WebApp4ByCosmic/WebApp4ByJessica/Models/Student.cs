using System.ComponentModel.DataAnnotations;

namespace WebApp4ByJessica.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(5, 100)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
