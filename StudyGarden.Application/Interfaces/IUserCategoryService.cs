using StudyGarden.Core.Models;

namespace StudyGarden.Application.Interfaces;

public interface IUserCategoryService
{
    Task<int> Create(UserCategory userCategory);
    Task<List<UserCategory>> GetAll(int userId);
    Task<int> Delete(int id);
    Task<int> Update(UserCategory userCategory);
    Task<UserCategory> GetById(int id);
}