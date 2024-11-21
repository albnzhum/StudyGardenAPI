using StudyGarden.Application.Interfaces.Abstractions;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Interfaces;

public interface IUserAchievementService : IService<UserAchievement>
{
    Task<List<UserAchievement>> GetByAchievementID(int achievementId);
}