using StudyGarden.Core.Models;

namespace StudyGarden.Core.Abstractions;

public interface IPlantRepository : IRepository<Plant>
{
    Task<List<Plant>> GetPlantsByPlantTypeID(int categoryId);
}