using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyGarden.Application.Interfaces.Abstractions;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.API.Endpoints.Abstractions;

public interface IEndpoint<T, in U> where T : IModel where U : IService<T>
{
    static abstract Task<IResult> Create([FromBody] T model, U service);
    static abstract Task<IResult> Update([FromBody] T model, U service);
    static abstract Task<IResult> Delete(int id, U service);
    static abstract Task<IResult> GetAll(U service, int userId = default);
    static abstract Task<IResult> Get(U service, int id = default);

}