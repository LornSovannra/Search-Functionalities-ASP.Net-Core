using Microsoft.EntityFrameworkCore;
using Search_Funct.Models;

namespace Search_Funct.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}
