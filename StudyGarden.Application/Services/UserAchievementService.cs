using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Services;

public class UserAchievementService(IRepository<UserAchievement> repository) : IUserAchievementService
{
    private readonly IRepository<UserAchievement> _repository = repository;
    public async Task<int> Create(UserAchievement userAchievement)
    {
        return await _repository.Create(userAchievement);
    }

    public async Task<int> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public async Task<int> Update(UserAchievement userAchievement)
    {
        return await _repository.Update(userAchievement);
    }

    public async Task<List<UserAchievement>> GetAll(int userId)
    {
        return await _repository.GetAll(userId);
    }

    public async Task<UserAchievement> Get(int id)
    {
        return await _repository.Get(id);
    }
}