using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class UserAchievementRepository(StudyGardenDbContext context) : IRepository<UserAchievement>
{
    private readonly StudyGardenDbContext _context = context;
    
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

    public async Task<int> Update(UserAchievement achievement)
    {
        await _context.UserAchievements
            .Where(ua => ua.ID == achievement.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(x => x.DateEarned, achievement.DateEarned));
        return achievement.ID;
    }

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

    public async Task<List<UserAchievement>> GetAll(int userId)
    {
        return await _context.UserAchievements
            .AsNoTracking()
            .Where(ua => ua.UserID == userId)
            .ToListAsync();
    }

    public async Task<UserAchievement> Get(int id)
    {
        return await _context.UserAchievements
            .AsNoTracking()
            .FirstOrDefaultAsync(ua => ua.ID == id) ?? throw new InvalidOperationException();
    }
}