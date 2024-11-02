using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class AchievementRepository(StudyGardenDbContext context) : IRepository<Achievement>
{
    private readonly StudyGardenDbContext _context = context;
    
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

    public async Task<int> Delete(int id)
    {
        await _context.Achievements
            .Where(achievement => achievement.ID == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<Achievement> Get(int id)
    {
        return await _context.Achievements
            .AsNoTracking()
            .FirstOrDefaultAsync(achievement => achievement.ID == id) ?? throw new InvalidOperationException();
    }

    public async Task<List<Achievement>> GetAll(int userId)
    {
        return await _context.Achievements
            .AsNoTracking()
            .ToListAsync();
    }
}