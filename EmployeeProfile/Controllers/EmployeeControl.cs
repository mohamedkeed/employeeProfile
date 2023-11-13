using EmployeeProfile.Data;
using EmployeeProfile.Models.Param;
using EmployeeProfile.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProfile.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmployeeControl : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeControl(EmployeeDbContext employeeDbContext)
        {
            this._employeeDbContext = employeeDbContext;
        }


        [HttpGet("Get All Employee")]
        public async Task<IActionResult> GetAllEmployee() {

            return Ok(await _employeeDbContext.Employees.ToListAsync()); 
        }


        [HttpGet("Get All Employee in Department")]
        public async Task<IActionResult> GetAllEmployeeinDepartment(int departmentid)
        {
            try
            {
                List<Employee> employees = _employeeDbContext.Employees.Where(i => i.DepartmentId == departmentid).ToList();
                return Ok(employees);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }


        [HttpGet("Get Employee")]
        public async Task<IActionResult> GetEmployee(int employeeid)
        {
            try
            {
                var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(i => i.Id == employeeid);
                if (employee == null) { return NotFound(); }
                return Ok(employee);
            }
            catch (Exception ex) { return BadRequest(ex.Message);  }

        }


        [HttpPost("Add Employee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeParam AddEmployeeRequest)
        {
            if (AddEmployeeRequest.DepartmentId != 1 && AddEmployeeRequest.DepartmentId != 2)
            {
                return BadRequest("department is not exist");
            }
            var Employee = new Employee()
            {
                
                Name = AddEmployeeRequest.Name,
                DateofBirth = AddEmployeeRequest.DateofBirth,
                PhoneNumber = AddEmployeeRequest.PhoneNumber,
                Email = AddEmployeeRequest.Email,
                Createdat = System.DateTime.Now,
                Salary = AddEmployeeRequest.Salary,
                DepartmentId = AddEmployeeRequest.DepartmentId
            };
            try
            {
                await _employeeDbContext.Employees.AddAsync(Employee);
                await _employeeDbContext.SaveChangesAsync();
                return Ok(Employee);
            }
            catch (Exception ex) { return BadRequest( ex.Message); }
 
        }


        [HttpPut("Update Employee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeParam UpdateEmployeeRequest)
        {
            var updatedemployee = await _employeeDbContext.Employees.FindAsync(UpdateEmployeeRequest.Id);
            if (updatedemployee == null) { return NotFound(); }
            if (UpdateEmployeeRequest.DepartmentId != 1 && UpdateEmployeeRequest.DepartmentId != 2)
            {
                return BadRequest("department is not exist");
            }
            updatedemployee.Name = UpdateEmployeeRequest.Name;
            updatedemployee.DateofBirth = UpdateEmployeeRequest.DateofBirth;
            updatedemployee.PhoneNumber = UpdateEmployeeRequest.PhoneNumber;
            updatedemployee.Email = UpdateEmployeeRequest.Email;
            updatedemployee.Updatedat = System.DateTime.Now;
            updatedemployee.Salary = UpdateEmployeeRequest.Salary;
            updatedemployee.DepartmentId = UpdateEmployeeRequest.DepartmentId;
            try
            {
                await _employeeDbContext.SaveChangesAsync();
                return Ok(updatedemployee);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpDelete("Delete Employee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var Deletedemployee = await _employeeDbContext.Employees.FindAsync(id);
            if (Deletedemployee == null) { return NotFound(); }
            try
            {
                _employeeDbContext.Remove(Deletedemployee);
                await _employeeDbContext.SaveChangesAsync();
                return Ok(Deletedemployee);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpPut("Increase Salary")]
        public async Task<IActionResult> UpdateSalary(UpdateSalaryParam UpdateSalaryRequest)
        {
            try
            {
                if (UpdateSalaryRequest == null)
                {
                    return BadRequest();
                }

                var employees = _employeeDbContext.Employees.Where(e => e.DepartmentId == UpdateSalaryRequest.DepartmentId);

                if (UpdateSalaryRequest.IncreaseType == "percent")
                {
                    foreach (var employee in employees)
                    {
                        employee.Salary *= (1 + UpdateSalaryRequest.IncreaseAmount / 100);
                    }
                }
                else if (UpdateSalaryRequest.IncreaseType == "fixed")
                {
                    foreach (var employee in employees)
                    {
                        employee.Salary += UpdateSalaryRequest.IncreaseAmount;
                    }
                }
                else
                {
                    return BadRequest("you should choose between percent or fixed");
                }

                await _employeeDbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


    }
}
