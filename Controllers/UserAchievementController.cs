using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyGarden.Common;
using StudyGarden.Entities;

namespace StudyGarden.Controllers;

[ApiController]
[Route("[controller]")]
public class UserAchievementController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserAchievementController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAllUserAchievements")]
    public async Task<IActionResult> GetAllUserAchievements()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var achievements = await _context.UserAchievements
            .Where(c => c.UserID == userId)
            .ToListAsync();
        return Ok(achievements);
    }

    [HttpPost("UnlockNewAchievement")]
    public async Task<IActionResult> UnlockNewAchievement([FromBody] Achievement achievement)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var newAchievement = new UserAchievement
        {
            UserID = userId,
            AchievementID = achievement.ID,
            DateEarned = DateTime.Today,
        };

        _context.UserAchievements.Add(newAchievement);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Achievement created successfully" });
    }
}