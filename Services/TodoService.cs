using Assignment.Data;
using Assignment.Model;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class TodoService : ITodoService
    {
        private readonly TaskDbContext _context;
        private readonly IConfiguration _configuration;

        public TodoService(TaskDbContext todoService, IConfiguration configuration)
        {
            _context = todoService;
            _configuration = configuration;
        }
        public async Task<bool>CreateRegister(Registration registration)
        {
            var EmailAlreadyExists = _context.Tasks1.SingleOrDefault(x => x.Email == registration.Email && x.Password == registration.Password);
            if (EmailAlreadyExists != null)
            {
                return false;
            }
            var userObj = new Registration()
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Email = registration.Email,
                IsActive = registration.IsActive,
                Password = registration.Password,
                Role = registration.Role

            };
            _context.Tasks1.Add(userObj);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Registration>> GetAll()
        {
            return await _context.Tasks1.ToListAsync();
        }
    }
}
