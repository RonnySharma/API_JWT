using API_JWT.Identity;
using API_JWT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            return Ok(_context.Employees.ToList());
        }
        [HttpPost]
        public IActionResult SaveEmployee([FromBody] employee employee)
        {
            if (employee == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] employee employee)
        {
            if (employee == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employeeInDb = _context.Employees.Find(id);
            if (employeeInDb == null) return NotFound();
            _context.Employees.Remove(employeeInDb);
            _context.SaveChanges();
            return Ok();
        }


    }
}
