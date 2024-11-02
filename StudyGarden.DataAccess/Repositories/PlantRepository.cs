using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class PlantRepository(StudyGardenDbContext context) : IRepository<Plant>
{
    private readonly StudyGardenDbContext _context = context;
    
    public async Task<int> Create(Plant plant)
    {
        try
        {
            var plantEntity = Plant.Create(plant.Name, plant.PlantTypeID, plant.IsUnlocked);

            await _context.Plants.AddAsync(plantEntity.plant);
            await _context.SaveChangesAsync();

            return plantEntity.plant.ID;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> Update(Plant plant)
    {
        await _context.Plants
            .Where(p => p.ID == plant.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(p => p.Name, plant.Name)
                .SetProperty(p => p.PlantTypeID, plant.PlantTypeID)
                .SetProperty(p => p.IsUnlocked, plant.IsUnlocked));

        return plant.ID;
    }

    public async Task<int> Delete(int id)
    {
        try
        {
            await _context.Plants
                .Where(p => p.ID == id)
                .ExecuteDeleteAsync();

            return id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<List<Plant>> GetAll(int userId)
    {
        return await _context.Plants
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Plant> Get(int id)
    {
        return await _context.Plants
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ID == id) ?? throw new InvalidOperationException();
    }
}