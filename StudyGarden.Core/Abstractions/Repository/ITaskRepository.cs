using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.Core.Abstractions;

public interface ITaskRepository : IRepository<Task>
{
    Task<List<Task>> GetByCategoryId(int categoryId);
    Task<List<Task>> GetByPlantId(int plantId);
    
}