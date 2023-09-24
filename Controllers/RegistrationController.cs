using Assignment.Data;
using Assignment.Model;
using Assignment.Services;
using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly TaskDbContext _taskDbContext;
        private readonly ITodoService _todoService;

        public RegistrationController(TaskDbContext taskDbContext, ITodoService todoService)
        {
            _taskDbContext = taskDbContext;
            _todoService = todoService;
        }
        [HttpPost("CreateRegister")]
        [AllowAnonymous]
        public async Task<ActionResult> Post(Registration user)
        {
            var data = await _todoService.CreateRegister(user);
            if (data)
            {
                return Ok("Record Save successfully");
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var data = await _todoService.GetAll();
            return Ok(data);
        }
    }
}
