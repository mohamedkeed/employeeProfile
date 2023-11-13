using EmployeeProfile.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EmployeeProfile.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasOne<Department>().WithMany().HasForeignKey(p =>p.DepartmentId);

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "HR department",

                },
                new Department
                {
                    Id=2,
                    Name = "Development department"
                }
                );


        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> departments { get; set; }


    }
}
