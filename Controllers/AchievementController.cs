using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyGarden.Common;

namespace StudyGarden.Controllers;

[ApiController]
[Route("[controller]")]
public class AchievementController : ControllerBase
{
    private readonly AppDbContext _context;

    public AchievementController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAllAchievements")]
    public async Task<IActionResult> GetAllAchievements()
    {
        var achievements = await _context.Achievements.ToListAsync();

        return Ok(achievements);
    }

    [HttpGet("GetAchievementByID/{id}")]
    public async Task<IActionResult> GetAchievementByID(int id)
    {
        var achievement = await _context.Achievements
            .FirstOrDefaultAsync(a => a.ID == id);
        
        if (achievement == null)
        {
            return NotFound(new { message = "Achievement not found or does not belong to the user." });
        }

        return Ok(achievement);
    }
}