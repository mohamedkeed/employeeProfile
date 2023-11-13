
using System.ComponentModel.DataAnnotations;

namespace EmployeeProfile.Models.Param
{
    public class UpdateEmployeeParam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
