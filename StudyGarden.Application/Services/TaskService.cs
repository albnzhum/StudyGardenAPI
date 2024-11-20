using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.Application.Services;

public class TaskService(IRepository<Task> repository) : ITaskService
{
    private readonly IRepository<Task> _repository = repository;
    
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
}