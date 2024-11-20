using StudyGarden.Core.Models;

namespace StudyGarden.Core.Abstractions;

public interface IUserAchievementRepository : IRepository<UserAchievement>
{
    Task<List<UserAchievement>> GetByAchivementID(int achievementId);
}