using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Services;

public class PlantTypeService(IPlantTypeRepository repository) : IPlantTypeService
{
    private readonly IPlantTypeRepository _repository = repository;
    public async Task<int> Create(PlantType plantType)
    {
        return await _repository.Create(plantType);
    }

    public async Task<int> Update(PlantType plantType)
    {
       return await _repository.Update(plantType);
    }

    public async Task<int> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public async Task<List<PlantType>> GetAll(int userid)
    {
        return await _repository.GetAll(0);
    }

    public async Task<PlantType> Get(int id)
    {
        return await _repository.Get(id);
    }
}