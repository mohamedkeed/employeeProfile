using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProfile.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public  string Name { get; set; }
        [Required]
        
        public DateTime DateofBirth { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required]
        public DateTime? Createdat { get; set; } 
        public DateTime? Updatedat { get; set; }
        [Required]
        public int Salary { get; set; }

        public int DepartmentId { get; set; }


    }
}
