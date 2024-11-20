using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Repositories;

public class FriendRepository(StudyGardenDbContext context) : IFriendRepository
{
    private readonly StudyGardenDbContext _context = context;
    
    public async Task<int> Create(Friend friend)
    {
        try
        {
            var friendEntity = Friend.Create(friend.UserID, friend.FriendID, friend.IsAccepted);

            await _context.Friends.AddAsync(friendEntity.friend);
            await _context.SaveChangesAsync();

            return friendEntity.friend.ID;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> Update(Friend friend)
    {
        await _context.Friends
            .Where(f => f.ID == friend.ID)
            .ExecuteUpdateAsync(set => set
                .SetProperty(x => x.IsAccepted, friend.IsAccepted));
        return friend.ID;
    }

    public async Task<int> Delete(int id)
    {
        try
        {
            await _context.Friends
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

    public async Task<Friend> Get(int id)
    {
        return await _context.Friends
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.ID == id) ?? throw new InvalidOperationException();
    }

    public async Task<List<Friend>> GetAll(int userId)
    {
        return await _context.Friends
            .AsNoTracking()
            .Where(f => f.UserID == userId)
            .ToListAsync();
    }
}