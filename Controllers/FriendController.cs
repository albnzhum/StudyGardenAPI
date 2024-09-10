using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyGarden.Common;
using StudyGarden.Entities;

namespace StudyGarden.Controllers;

[ApiController]
[Route("[controller]")]
public class FriendController : ControllerBase
{
    private readonly AppDbContext _context;

    public FriendController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAllAcceptedFriends")]
    public async Task<IActionResult> GetAllAcceptedFriends()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var friends = await _context.Friend
            .Where(fr => fr.UserID == userId)
            .Where(fr => fr.IsAccepted)
            .ToListAsync();

        return Ok(friends);
    }

    [HttpGet("GetAllFriendsRequested")]
    public async Task<IActionResult> GetAllFriendRequested()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var friends = await _context.Friend
            .Where(fr => fr.UserID == userId)
            .Where(fr => !fr.IsAccepted)
            .ToListAsync();

        return Ok(friends);
    }
    
    [Authorize(Policy = "IsOwner")]
    [HttpGet("GetAllFriends")]
    public async Task<IActionResult> GetAllFriends()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var friends = await _context.Friend
            .Where(fr => fr.UserID == userId)
            .ToListAsync();

        return Ok(friends);
    }

    [Authorize(Policy = "IsOwner")]
    [HttpPut("AcceptFriend/{id}")]
    public async Task<IActionResult> AcceptFriend(int id, [FromBody] Friend friend)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if (id != friend.ID)
        {
            return BadRequest("Friend ID mismatch.");
        }

        var requestedFriend = await _context.Friend.FindAsync(id);
        if (requestedFriend == null)
        {
            return NotFound();
        }

        requestedFriend.IsAccepted = true;

        _context.Friend.Update(requestedFriend);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [Authorize(Policy = "IsOwner")]
    [HttpPut("DeleteFriend/{id}")]
    public async Task<IActionResult> DeleteFriend(int id, [FromBody] Friend friend)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if (id != friend.ID)
        {
            return BadRequest("Friend ID mismatch.");
        }

        var requestedFriend = await _context.Friend.FindAsync(id);
        if (requestedFriend == null)
        {
            return NotFound();
        }

        requestedFriend.IsAccepted = false;

        _context.Friend.Update(requestedFriend);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}