using StudyGarden.Application.Interfaces.Abstractions;
using StudyGarden.Core.Abstractions;
using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.Application.Interfaces;

public interface ITaskService : IService<Task>
{
    Task<List<Task>> GetByCategoryId(int categoryId);
    Task<List<Task>> GetByPlantId(int plantId);
}