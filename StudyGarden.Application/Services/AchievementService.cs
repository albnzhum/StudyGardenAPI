using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Services;

public class AchievementService(IAchievementRepository repository) : IAchievementService
{
    private readonly IAchievementRepository _repository = repository;
    public async Task<int> Create(Achievement achievement)
    {
        return await _repository.Create(achievement);
    }

    public async Task<int> Update(Achievement achievement)
    {
        return await _repository.Update(achievement);
    }

    public async Task<int> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public async Task<List<Achievement>> GetAll(int userId)
    {
        return await _repository.GetAll();
    }

    public async Task<Achievement> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<Plant> GetPlant(int achievementId)
    {
        return await _repository.GetPlant(achievementId);
    }
}