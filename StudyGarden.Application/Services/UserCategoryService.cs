using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Services;

public class UserCategoryService(IRepository<UserCategory> categoryRepository) : IUserCategoryService
{
    private readonly IRepository<UserCategory> _categoryRepository = categoryRepository;
    public async Task<int> Create(UserCategory userCategory)
    {
        return await _categoryRepository.Create(userCategory);
    }

    public async Task<List<UserCategory>> GetAll(int userId)
    {
        return await _categoryRepository.GetAll(userId);
    }

    public async Task<int> Delete(int id)
    {
        return await _categoryRepository.Delete(id);
    }

    public async Task<int> Update(UserCategory userCategory)
    {
        return await _categoryRepository.Update(userCategory);
    }

    public async Task<UserCategory> GetById(int id)
    {
        return await _categoryRepository.Get(id);
    }
}