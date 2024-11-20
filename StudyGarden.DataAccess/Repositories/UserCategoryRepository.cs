using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class UserCategoryRepository(StudyGardenDbContext context) : IUserCategoryRepository
{
    private readonly StudyGardenDbContext _context = context;
    
    /// <summary>
    /// Создание сущности UserCategory и добавление в базу данных
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task<int> Create(UserCategory category)
    {
        try
        {
            var userCategory = UserCategory.Create(category.UserID, category.PlantTypeID, category.Title);

            await _context.UserCategory.AddAsync(userCategory.userCategory);
            await _context.SaveChangesAsync();

            return userCategory.userCategory.ID;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Обновление существующей сущности UserCategory
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task<int> Update(UserCategory category)
    {
        await _context.UserCategory
            .Where(uc => uc.ID == category.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(x => x.Title, category.Title)
                .SetProperty(x => x.PlantTypeID, category.PlantTypeID));
        return category.ID;
    }

    /// <summary>
    /// Получение всех сущностей UserCategory с указанным PlantTypeID
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns></returns>
    public async Task<List<UserCategory>> GetAllByType(int typeId)
    {
        return await _context.UserCategory
            .Where(uc => uc.PlantTypeID == typeId)
            .ToListAsync();
    }

    /// <summary>
    /// Поиск сущности UserCategory по ID и удаление из базы данных
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(int id)
    {
        try
        {
            await _context.UserCategory
                .Where(uc => uc.ID == id)
                .ExecuteDeleteAsync();

            return id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Получение всех сущностей UserCategory с указанным UserID
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<UserCategory>> GetAll(int userId)
    {
        return await _context.UserCategory
            .AsNoTracking()
            .Where(uc => uc.UserID == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Поиск и получение сущности UserCategory по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<UserCategory> Get(int id)
    {
        return await _context.UserCategory
            .AsNoTracking()
            .FirstOrDefaultAsync(uc => uc.ID == id) ?? throw new InvalidOperationException();
    }
}