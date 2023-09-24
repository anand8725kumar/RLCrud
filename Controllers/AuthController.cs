using Assignment.Data;
using Assignment.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TaskDbContext _autherDbContext;

        public AuthController(TaskDbContext autherDbContext)
        {
            _autherDbContext = autherDbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crud>>> GetEmployees()
        {
            if (_autherDbContext.Task2 == null)
            {
                return NotFound();
            }
            return await _autherDbContext.Task2.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Crud>> GetEmployee(int id)
        {
            if (_autherDbContext.Task2 == null)
            {
                return NotFound();
            }
            var employee = await _autherDbContext.Task2.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        [HttpPost]
        public async Task<ActionResult<Crud>> postEmployee(Crud employee)
        {
            _autherDbContext.Task2.Add(employee);
            await _autherDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee),
                new { id = employee.Id }, employee);
        }

        [HttpPut]
        public async Task<IActionResult> PutEmployee(int id, Crud employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            _autherDbContext.Entry(employee).State = EntityState.Modified;
            try
            {
                await _autherDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool EmployeeAvailable(int id)
        {
            return (_autherDbContext.Task2?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_autherDbContext.Task2 == null)
            {
                return NotFound();
            }
            var employee = await _autherDbContext.Task2.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _autherDbContext.Task2.Remove(employee);
            await _autherDbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
