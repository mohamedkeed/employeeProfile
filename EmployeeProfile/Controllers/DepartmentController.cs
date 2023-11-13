using EmployeeProfile.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProfile.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public DepartmentController(EmployeeDbContext employeeDbContext)
        {
            this._employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        [Route("Get All Department")]
        public async Task<IActionResult> GetAllEmployee()
        {
            return Ok(await _employeeDbContext.departments.ToListAsync());
        }
    }
}
