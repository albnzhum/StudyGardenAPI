using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class PlantRepository(StudyGardenDbContext context) : IPlantRepository
{
    private readonly StudyGardenDbContext _context = context;
    
    /// <summary>
    /// Создание сущности Plant и добавление ее в базу данных
    /// </summary>
    /// <param name="plant"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Обновление существующей сущности Plant
    /// </summary>
    /// <param name="plant"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Поиск сущности Plant по ID и удаление из базы данных
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Получение всех сущностей Plant
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<Plant>> GetAll(int userId)
    {
        return await _context.Plants
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Получение всех сущностей Plant с указанным PlantTypeID
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public async Task<List<Plant>> GetPlantsByPlantTypeID(int categoryId)
    {
        return await _context.Plants
            .AsNoTracking()
            .Where(p => p.PlantTypeID == categoryId)
            .ToListAsync();
    }

    /// <summary>
    /// Получение сущности Plant по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<Plant> Get(int id)
    {
        return await _context.Plants
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ID == id) ?? throw new InvalidOperationException();
    }
}