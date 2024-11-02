using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGarden.DataAccess.Repositories;

public class UserRepository(StudyGardenDbContext context) : IUserRepository
{
    private readonly StudyGardenDbContext _context = context;

    public async Task<int> Create(User user)
    {
        var userEntity = User.Create(user.Login, user.HashedPassword);

        await _context.Users.AddAsync(userEntity.user);
        await _context.SaveChangesAsync();

        return userEntity.user.ID;
    }

    public async Task<User> GetByLogin(string login)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Login == login) ?? throw new Exception();
        return userEntity;
    }

    public async Task<List<User>> GetAll()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> Update(int id, string login, string password)
    {
        await _context.Users
            .Where(user => user.ID == id)
            .ExecuteUpdateAsync(set => set
                .SetProperty(user => user.Login, user => login)
                .SetProperty(user => user.HashedPassword, user => password));
        
        return id;
    }

    public async Task<int> Delete(int id)
    {
        await _context.Users
            .Where(user => user.ID == id)
            .ExecuteDeleteAsync();

        return id;
    }
}