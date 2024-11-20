using StudyGarden.Core.Models;

namespace StudyGarden.Core.Abstractions;

public interface IAchievementRepository : IRepository<Achievement>
{
    Task<Plant> GetPlant(int achievementId);
}