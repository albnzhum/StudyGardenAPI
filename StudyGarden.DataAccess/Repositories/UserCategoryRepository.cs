using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class UserCategoryRepository(StudyGardenDbContext context) : IRepository<UserCategory>
{
    private readonly StudyGardenDbContext _context = context;
    
    public async Task<int> Create(UserCategory category)
    {
        try
        {
            var userCategory = UserCategory.Create(category.UserID, category.PlantTypeID, category.Title);

            await _context.UserCategory.AddAsync(userCategory.userCategory);
            await _context.SaveChangesAsync();

            return userCategory.userCategory.ID;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> Update(UserCategory category)
    {
        await _context.UserCategory
            .Where(uc => uc.ID == category.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(x => x.Title, category.Title)
                .SetProperty(x => x.PlantTypeID, category.PlantTypeID));
        return category.ID;
    }

    public async Task<int> Delete(int id)
    {
        try
        {
            await _context.UserCategory
                .Where(uc => uc.ID == id)
                .ExecuteDeleteAsync();

            return id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<List<UserCategory>> GetAll(int userId)
    {
        return await _context.UserCategory
            .AsNoTracking()
            .Where(uc => uc.UserID == userId)
            .ToListAsync();
    }

    public async Task<UserCategory> Get(int id)
    {
        return await _context.UserCategory
            .AsNoTracking()
            .FirstOrDefaultAsync(uc => uc.ID == id) ?? throw new InvalidOperationException();
    }
}