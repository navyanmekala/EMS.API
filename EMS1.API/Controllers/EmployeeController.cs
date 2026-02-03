using Microsoft.AspNetCore.Mvc;
using EMS1.API.Models;
using EMS1.API.Data;

namespace EMS1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
