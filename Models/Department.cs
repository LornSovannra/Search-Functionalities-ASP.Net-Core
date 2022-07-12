using System.ComponentModel.DataAnnotations;

namespace Search_Funct.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
