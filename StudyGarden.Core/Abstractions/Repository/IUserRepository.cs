using StudyGarden.Core.Models;

namespace StudyGarden.Core.Abstractions;

public interface IUserRepository
{
    Task<int> Create(User user);
    Task<User> GetByLogin(string login);
    Task<List<User>> GetAll();
    Task<int> Update(int id, string login, string password);
    Task<int> Delete(int id);
}