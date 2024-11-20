using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Services;

public class GardenService(IRepository<Garden> repository) : IGardenService
{
    private readonly IRepository<Garden> _repository = repository;
    public async Task<int> Create(Garden garden)
    {
        return await _repository.Create(garden);
    }

    public async Task<int> Update(Garden garden)
    {
        return await _repository.Update(garden);
    }

    public async Task<int> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public async Task<List<Garden>> GetAll(int userId)
    {
        return await _repository.GetAll(0);
    }

    public async Task<List<Garden>> GetByUserId(int userId)
    {
        return await _repository.GetAll(userId);
    }

    public async Task<Garden> Get(int id)
    {
        return await _repository.Get(id);
    }
}