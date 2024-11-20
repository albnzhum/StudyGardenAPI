using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class UserAchievementRepository(StudyGardenDbContext context) : IUserAchievementRepository
{
    private readonly StudyGardenDbContext _context = context;
    
    /// <summary>
    /// Создание сущности UserAchievement
    /// </summary>
    /// <param name="achievement"></param>
    /// <returns></returns>
    public async Task<int> Create(UserAchievement achievement)
    {
        try
        {
            var userAchievement = UserAchievement.Create(achievement.UserID, 
                achievement.AchievementID, achievement.DateEarned);

            await _context.UserAchievements.AddAsync(userAchievement.userAchievement);
            await _context.SaveChangesAsync();

            return userAchievement.userAchievement.ID;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Обновление существующей сущности UserAchievement
    /// </summary>
    /// <param name="achievement"></param>
    /// <returns></returns>
    public async Task<int> Update(UserAchievement achievement)
    {
        await _context.UserAchievements
            .Where(ua => ua.ID == achievement.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(x => x.DateEarned, achievement.DateEarned));
        return achievement.ID;
    }

    /// <summary>
    /// Поиск всех сущностей UserAchievement с указанным ID сущности Achievement
    /// </summary>
    /// <param name="achievementId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<UserAchievement>> GetByAchivementID(int achievementId)
    {
        return await _context.UserAchievements
            .Where(achievement => achievement.AchievementID == achievementId)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Поиск сущности UserAchievement по ID и ее удаление
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(int id)
    {
        try
        {
            await _context.UserAchievements
                .Where(f => f.ID == id)
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
    /// Поиск всех сущностей UserAchievement по указанному ID пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<UserAchievement>> GetAll(int userId)
    {
        return await _context.UserAchievements
            .AsNoTracking()
            .Where(ua => ua.UserID == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Поиск сущности UserAchievement по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<UserAchievement> Get(int id)
    {
        return await _context.UserAchievements
            .AsNoTracking()
            .FirstOrDefaultAsync(ua => ua.ID == id) ?? throw new InvalidOperationException();
    }
}