using StudyGarden.Application.Interfaces;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.Application.Services;

public class UserCategoryService(IUserCategoryRepository categoryRepository) : IUserCategoryService
{
    private readonly IUserCategoryRepository _categoryRepository = categoryRepository;
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

    public async Task<UserCategory> Get(int id)
    {
        return await _categoryRepository.Get(id);
    }

    public async Task<List<UserCategory>> GetAllByType(int typeId)
    {
        return await _categoryRepository.GetAllByType(typeId);
    }
}