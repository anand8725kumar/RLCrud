using Assignment.Model;

namespace Assignment.Services
{
    public interface ITodoService
    {
        Task<bool> CreateRegister(Registration user);
        Task<List<Registration>> GetAll();
    }
}
