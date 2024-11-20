using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.DataAccess.Repositories;

public class TaskRepository(StudyGardenDbContext context) : ITaskRepository
{
    private readonly StudyGardenDbContext _context = context;
    
    /// <summary>
    /// Создание сущности Task и добавление ее в базу данных
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public async Task<int> Create(Task task)
    {
        try
        {
            var taskEntity = Task.Create(task.Title, task.Description, task.UserID, task.PlantID, task.CategoryID,
                task.CreatedDate, task.DueDate, task.Status, task.Priority);
            
            await _context.Tasks.AddAsync(taskEntity.task);
            await _context.SaveChangesAsync();

            return taskEntity.task.ID;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Поиск и обновление сущности Task
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public async Task<int> Update(Task task)
    {
        await _context.Tasks
            .Where(t => t.ID == task.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(t => t.Title, task.Title)
                .SetProperty(t => t.Description, task.Description)
                .SetProperty(t => t.CategoryID, task.CategoryID)
                .SetProperty(t => t.DueDate, task.DueDate)
                .SetProperty(t => t.Status, task.Status)
                .SetProperty(t => t.Priority, task.Priority)
                .SetProperty(t => t.PlantID, task.PlantID));

        return task.ID;
    }

    /// <summary>
    /// Поиск сущности Task по ID и удаление из базы данных
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(int id)
    {
        try
        {
            await _context.Tasks
                .Where(t => t.ID == id)
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
    /// Получение всех сущностей Task
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<Task>> GetAll(int userId)
    {
        return await _context.Tasks
            .AsNoTracking()
            .Where(t => t.UserID == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Поиск и получение сущности Task по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<Task> Get(int id)
    {
        return await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.ID == id) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// Поиск и получение сущностей Task по указанному CategoryID 
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<Task>> GetByCategoryId(int categoryId)
    {
        return await _context.Tasks
            .Where(task => task.CategoryID == categoryId)
            .ToListAsync();
    }

    /// <summary>
    /// Поиск и получение сущностей Task по указанному PlantID
    /// </summary>
    /// <param name="plantId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<Task>> GetByPlantId(int plantId)
    {
        return await _context.Tasks
            .Where(task => task.PlantID == plantId)
            .ToListAsync();
    }
}