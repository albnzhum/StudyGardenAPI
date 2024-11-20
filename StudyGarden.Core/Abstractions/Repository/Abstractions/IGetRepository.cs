using System.Runtime.InteropServices;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Abstractions;

public interface IGetRepository<T> where T : IModel
{
    Task<List<T>> GetAll([Optional] int userId);
    Task<T> Get(int id);
}