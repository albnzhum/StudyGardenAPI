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

    /// <summary>
    /// Создание сущности User и добавление в базу данных
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<int> Create(User user)
    {
        var userEntity = User.Create(user.Login, user.HashedPassword);

        await _context.Users.AddAsync(userEntity.user);
        await _context.SaveChangesAsync();

        return userEntity.user.ID;
    }

    /// <summary>
    /// Получение сущности User по указанному логину
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<User> GetByLogin(string login)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Login == login) ?? throw new Exception();
        return userEntity;
    }

    /// <summary>
    /// Получение всех сущностей User
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<User>> GetAll(int userId = default)
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Обновление существующей сущности User
    /// </summary>
    /// <param name="newUser"></param>
    /// <returns></returns>
    public async Task<int> Update(User newUser)
    {
        await _context.Users
            .Where(user => user.ID == newUser.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(user => user.Login, user => newUser.Login)
                .SetProperty(user => user.HashedPassword, user => newUser.HashedPassword));
        
        return newUser.ID;
    }

    /// <summary>
    /// Поиск сущности User по ID и удаление из базы данных
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(int id)
    {
        await _context.Users
            .Where(user => user.ID == id)
            .ExecuteDeleteAsync();

        return id;
    }

    /// <summary>
    /// Поиск сущности User по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<User> Get(int id)
    {
        return await _context.Users
            .Where(user => user.ID == id)
            .AsNoTracking()
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException();
    }
}