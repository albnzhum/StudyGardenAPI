using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Abstractions;

public interface IUpdateRepository<T> where T : IModel
{
    Task<int> Update(T entity);
}