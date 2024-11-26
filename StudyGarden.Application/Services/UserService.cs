using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;
using StudyGarden.Infrastructure.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace StudyGarden.Application.Services;

public class UserService(IPasswordHasher passwordHasher,
    IUserRepository userRepository, IJwtProvider jwtProvider) : IUserService
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUserRepository _usersRepository = userRepository;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task Register(string login, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var (user, error) = User.Create(login, hashedPassword);

        if (!string.IsNullOrEmpty(error))
        {
            throw new Exception(error);
        }

        await _usersRepository.Create(user);
    }

    public async Task<string> Login(string login, string password)
    {
        var user = await _usersRepository.GetByLogin(login);

        var result = _passwordHasher.Verify(password, user.HashedPassword);

        if (result == false)
        {
            throw new Exception("Failed to login");
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task<List<User>> GetAll(int userId = default)
    {
        return await _usersRepository.GetAll();
    }

    public async Task<int> GetUserID(string login, string password)
    {
        return await _usersRepository.GetUserID(login, password);
    }

    public async Task<User> Get(int id)
    {
         return await _usersRepository.Get(id);
    }

    public async Task<int> Create(User user)
    {
        return await _usersRepository.Create(user);
    }

    public async Task<int> Update(User user)
    {
        return await _usersRepository.Update(user);
    }
    
    public async Task<int> Delete(int id)
    {
        return await _usersRepository.Delete(id);
    }
}