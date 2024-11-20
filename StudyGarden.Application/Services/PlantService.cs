using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Services;

public class PlantService(IRepository<Plant> repository) : IPlantService
{
    private readonly IRepository<Plant> _repository = repository;

    public async Task<int> Create(Plant plant)
    {
        return await _repository.Create(plant);
    }

    public async Task<int> Update(Plant plant)
    {
        return await _repository.Update(plant);
    }

    public async Task<int> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public async Task<List<Plant>> GetAll(int userId)
    {
        return await _repository.GetAll(0);
    }

    public async Task<Plant> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<List<Plant>> GetAllByCategoryId(int categoryId)
    {
        return await _repository.GetAll(0);
    }
}