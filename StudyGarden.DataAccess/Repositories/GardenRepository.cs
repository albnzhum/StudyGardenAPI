using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class GardenRepository(StudyGardenDbContext context) : IRepository<Garden>
{
    private readonly StudyGardenDbContext _context = context;
    
    public async Task<int> Create(Garden garden)
    {
        try
        {
            var gardenEntity = Garden.Create(garden.UserID, garden.TaskID, garden.PlantID,
                garden.GrowthStage, garden.PositionX, garden.PositionY, garden.PositionZ);

            await _context.Garden.AddAsync(gardenEntity.garden);
            await _context.SaveChangesAsync();

            return gardenEntity.garden.ID;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> Update(Garden garden)
    {
        await _context.Garden
            .Where(g => g.ID == garden.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(g => g.TaskID, garden.TaskID)
                .SetProperty(g => g.PlantID, garden.PlantID)
                .SetProperty(g => g.GrowthStage, garden.GrowthStage)
                .SetProperty(g => g.PositionX, garden.PositionX)
                .SetProperty(g => g.PositionY, garden.PositionY)
                .SetProperty(g => g.PositionZ, garden.PositionZ));
        return garden.ID;
    }

    public async Task<int> Delete(int id)
    {
        try
        {
            await _context.Garden
                .Where(g => g.ID == id)
                .ExecuteDeleteAsync();

            return id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Garden> Get(int id)
    {
        return await _context.Garden
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.ID == id) ?? throw new InvalidOperationException();
    }

    public async Task<List<Garden>> GetAll(int userId)
    {
        return await _context.Garden
            .AsNoTracking()
            .Where(g => g.UserID == userId)
            .ToListAsync();
    }
}