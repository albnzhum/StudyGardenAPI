using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Abstractions;

public interface IDeleteRepository<T> where T : IModel
{
    Task<int> Delete(int id);
}