using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.Application.Services;

public class TaskService(ITaskRepository repository) : ITaskService
{
    private readonly ITaskRepository _repository = repository;
    
    public async Task<int> Create(Task task)
    {
        return await _repository.Create(task);
    }

    public async Task<int> Update(Task task)
    {
        return await _repository.Update(task);
    }

    public async Task<int> Delete(int id)
    {
       return await _repository.Delete(id);
    }

    public async Task<List<Task>> GetAll(int userId)
    {
        return await _repository.GetAll(userId);
    }

    public async Task<Task> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<List<Task>> GetByCategoryId(int categoryId)
    {
        return await _repository.GetByCategoryId(categoryId);
    }

    public async Task<List<Task>> GetByPlantId(int plantId)
    {
        return await _repository.GetByPlantId(plantId);
    }
}