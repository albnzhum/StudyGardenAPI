using StudyGarden.Application.Interfaces.Abstractions;
using StudyGarden.Contracts.UserRequests;
using StudyGarden.Core.Models;
using Task = System.Threading.Tasks.Task;

namespace StudyGarden.Application.Interfaces;

public interface IUserService : IService<User>
{
    Task<LoginResponse> Login(string login, string password);
    Task Register(string login, string password);
}