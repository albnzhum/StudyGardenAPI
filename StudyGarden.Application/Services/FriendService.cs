using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Services;

public class FriendService(IRepository<Friend> repository) : IFriendService
{
    private readonly IRepository<Friend> _repository = repository;
    public async Task<int> Create(Friend friend)
    {
        return await _repository.Create(friend);
    }

    public async Task<int> Update(Friend friend)
    {
        return await _repository.Update(friend);
    }

    public async Task<int> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public async Task<List<Friend>> GetAll(int userId)
    {
        return await _repository.GetAll();
    }

    public async Task<Friend> Get(int id)
    {
        return await _repository.Get(id);
    }
}