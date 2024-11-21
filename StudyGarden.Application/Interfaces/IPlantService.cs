using StudyGarden.Application.Interfaces.Abstractions;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;
using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.Application.Interfaces;

public interface IPlantService : IService<Plant>
{
    Task<List<Plant>> GetPlantsByPlantTypeID(int categoryId);
}