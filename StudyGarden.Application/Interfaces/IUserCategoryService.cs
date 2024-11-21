using StudyGarden.Application.Interfaces.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Interfaces;

public interface IUserCategoryService : IService<UserCategory>
{
    Task<List<UserCategory>> GetAllByType(int typeId);
}