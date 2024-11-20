using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Abstractions;

public interface ICreateRepository<T> where T : IModel
{
    Task<int> Create(T entity);
}