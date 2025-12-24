using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiByJessica.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto ID
        public int StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }
}
