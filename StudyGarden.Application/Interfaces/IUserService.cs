using StudyGarden.Core.Models;
using Task = System.Threading.Tasks.Task;

namespace StudyGarden.Application.Interfaces;

public interface IUserService
{
    Task<string> Login(string login, string password);
    Task Register(string login, string password);
    
    Task<List<User>> GetAllUsers();

    Task<int> CreateUser(User user);

    Task<int> UpdateUser(int id, string login, string password);

    Task<int> DeleteUser(int id);
}