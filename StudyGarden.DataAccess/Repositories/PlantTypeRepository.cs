using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class PlantTypeRepository(StudyGardenDbContext context) : ICreateRepository<PlantType>, IDeleteRepository<PlantType>, IUpdateRepository<PlantType>
{
    private readonly StudyGardenDbContext _context = context;
    
    public async Task<int> Create(PlantType plantType)
    {
        try
        {
            var plantTypeEntity = PlantType.Create(plantType.Name);

            await _context.PlantsType.AddAsync(plantTypeEntity.plantType);
            await _context.SaveChangesAsync();

            return plantTypeEntity.plantType.ID;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> Delete(int id)
    {
        try
        {
            await _context.PlantsType
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

    public async Task<List<PlantType>> GetAll()
    {
        return await _context.PlantsType
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PlantType> GetById(int id)
    {
        return await _context.PlantsType
            .AsNoTracking()
            .FirstOrDefaultAsync(pt => pt.ID == id) ?? throw new InvalidOperationException();
    }

    public async Task<int> Update(PlantType plantType)
    {
        await _context.PlantsType
            .Where(pt => pt.ID == plantType.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(x => x.Name, plantType.Name));
        return plantType.ID;
    }
}