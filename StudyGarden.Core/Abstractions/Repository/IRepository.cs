using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Abstractions;

public interface IRepository<T> : ICreateRepository<T>, IGetRepository<T>, IDeleteRepository<T>, IUpdateRepository<T> where T : IModel
{
    
}