using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class AchievementRepository(StudyGardenDbContext context) : IAchievementRepository
{
    private readonly StudyGardenDbContext _context = context;
    
    /// <summary>
    /// Создание сущности Achievement и добавление в базу данных
    /// </summary>
    /// <param name="achievement"></param>
    /// <returns></returns>
    public async Task<int> Create(Achievement achievement)
    {
        try
        {
            var achievementEntity = Achievement.Create(achievement.Title, achievement.PlantID);
            
            await _context.Achievements.AddAsync(achievementEntity.achievement);
            await _context.SaveChangesAsync();

            return achievementEntity.achievement.ID;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Обновление существующей сущности Achievement
    /// </summary>
    /// <param name="achievement"></param>
    /// <returns></returns>
    public async Task<int> Update(Achievement achievement)
    {
        await _context.Achievements
            .Where(a => a.ID == achievement.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(achieve => achieve.Title, achievement.Title)
                .SetProperty(achieve => achieve.PlantID, achievement.PlantID)
                .SetProperty(achieve => achieve.Plant, achievement.Plant));
        
        return achievement.ID;
    }

    /// <summary>
    /// Поиск сущности Achievement по ID и получение сущности Plant
    /// </summary>
    /// <param name="achievementId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<Plant> GetPlant(int achievementId)
    {
        return await _context.Achievements
            .Where(achievement => achievement.ID == achievementId)
            .Select(achievement => achievement.Plant)
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// Удаление сущности Achievement
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(int id)
    {
        await _context.Achievements
            .Where(achievement => achievement.ID == id)
            .ExecuteDeleteAsync();

        return id;
    }

    /// <summary>
    /// Получение сущности Achievement по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<Achievement> Get(int id)
    {
        return await _context.Achievements
            .AsNoTracking()
            .FirstOrDefaultAsync(achievement => achievement.ID == id) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// Получение всех сущностей Achievement
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<Achievement>> GetAll(int userId = default)
    {
        return await _context.Achievements
            .AsNoTracking()
            .ToListAsync();
    }
}