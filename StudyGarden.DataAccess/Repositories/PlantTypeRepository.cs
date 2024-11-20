using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class PlantTypeRepository(StudyGardenDbContext context) : IPlantTypeRepository
{
    private readonly StudyGardenDbContext _context = context;
    
    /// <summary>
    /// Создание сущности PlantType и добавление ее в базу данных
    /// </summary>
    /// <param name="plantType"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Поиск существующей сущности PlantType по ID и удаление из базы данных
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Получение всех сущностей PlantType
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<PlantType>> GetAll(int userId)
    {
        return await _context.PlantsType
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Поиск и получение сущности PlantType по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<PlantType> Get(int id)
    {
        return await _context.PlantsType
            .AsNoTracking()
            .FirstOrDefaultAsync(pt => pt.ID == id) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// Поиск и обновление сущности PlantType
    /// </summary>
    /// <param name="plantType"></param>
    /// <returns></returns>
    public async Task<int> Update(PlantType plantType)
    {
        await _context.PlantsType
            .Where(pt => pt.ID == plantType.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(x => x.Name, plantType.Name));
        return plantType.ID;
    }
}