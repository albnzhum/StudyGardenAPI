using StudyGarden.Core.Models;

namespace StudyGarden.Core.Abstractions;

public interface IUserCategoryRepository : IRepository<UserCategory>
{
    Task<List<UserCategory>> GetAllByType(int typeId);
}