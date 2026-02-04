using Microsoft.AspNetCore.Mvc;
using EMS1.API.Models;
using EMS1.API.Data;
using Microsoft.AspNetCore.Authorization;

namespace EMS1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_context.Employees.ToList());
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return Ok(emp);
        }

        [HttpGet("Id")]
        public IActionResult GetbyId(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
            {
                return NotFound("NO Employees with" + id);
            }
            return Ok(emp);
        }

        [HttpPut("id")]
        public IActionResult Update(int id, Employee updatedEmp)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
            {
                return NotFound("Employee not found");
            }
            emp.Name = updatedEmp.Name;
            emp.Email = updatedEmp.Email;
            emp.Department = updatedEmp.Department;
            emp.Salary = updatedEmp.Salary;
            _context.SaveChanges();
            return Ok(emp);
        }

        [HttpDelete("id")]
        public IActionResult DeleteById(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
            {
                return NotFound("Employee not found");
            }
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return Ok("Employee deleted successfully");


        }
    }
}
