using System.ComponentModel.DataAnnotations;

namespace Search_Funct.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
