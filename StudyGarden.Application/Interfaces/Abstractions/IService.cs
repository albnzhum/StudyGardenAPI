using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Application.Interfaces.Abstractions;

public interface IService<T> where T : IModel
{
    Task<int> Create(T model);
    Task<int> Update(T model);
    Task<int> Delete(int id);
    Task<List<T>> GetAll(int userId = default);
    Task<T> Get(int id);
}