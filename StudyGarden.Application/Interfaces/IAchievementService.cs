using StudyGarden.Application.Interfaces.Abstractions;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Interfaces;

public interface IAchievementService : IService<Achievement>
{
    Task<Plant> GetPlant(int achievementId);
}