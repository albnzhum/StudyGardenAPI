using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudyGarden.Common;
using StudyGarden.Entities;
using StudyGarden.Hub;

namespace StudyGarden.Controllers;

[ApiController]
[Route("[controller]")]
public class GardenController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHubContext<GardenHub> _hubContext;  // Внедряем IHubContext
    
    public GardenController(AppDbContext context, IHubContext<GardenHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }
    
    [HttpGet("ViewFriendGarden/{id}")]
    public async Task<IActionResult> ViewFriendGarden(int id)
    {
        var garden = await _context.Garden
            .Where(g => g.UserID == id)
            .ToListAsync();
        
        if (garden == null)
        {
            return NotFound("Garden not found for the specified user.");
        }

        return Ok(garden);
    }

    [HttpGet("LoadUserGarden")]
    public async Task<IActionResult> LoadUserGarden()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var garden = await _context.Garden
            .Where(g => g.UserID == userId)
            .ToListAsync();

        return Ok(garden);
    }

    [Authorize(Policy = "IsOwner")]
    [HttpPost("AddPlantToGarden")]
    public async Task<IActionResult> AddPlantToGarden([FromBody] Garden garden)
    {
        var userID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var newGarden = new Garden()
        {
            GrowthStage = garden.GrowthStage,
            PlantID = garden.PlantID,
            TaskID = garden.TaskID,
            UserID = userID,
            X = garden.X,
            Y = garden.Y,
            Z = garden.Z
        };

        _context.Garden.Add(newGarden);
        await _context.SaveChangesAsync();
        
        await _hubContext.Clients.Group(userID.ToString()).SendAsync("ReceiveGardenUpdate", userID);

        return Ok(new { message = "Plant added successfully" });
    }

    [Authorize(Policy = "IsOwner")]
    [HttpPut("UpdateGarden")]
    public async Task<IActionResult> UpdateGarden(int id, [FromBody] Garden garden)
    {
        if (id != garden.ID)
        {
            return BadRequest("Garden ID is mismatch");
        }

        ;var existingGarden = await _context.Garden.FindAsync(id);
        if (existingGarden == null)
        {
            return NotFound();
        }

        existingGarden.X = garden.X;
        existingGarden.Y = garden.Y;
        existingGarden.Z = garden.Z;

        existingGarden.GrowthStage = garden.GrowthStage;

        _context.Garden.Update(existingGarden);
        await _context.SaveChangesAsync();
        
        await _hubContext.Clients.Group(existingGarden.UserID.ToString()).SendAsync("ReceiveGardenUpdate", existingGarden);
        
        return NoContent();
    }

    [Authorize(Policy = "IsOwner")]
    [HttpPut("UpdatePlantGrowthStage/{id}")]
    public async Task<IActionResult> UpdatePlantGrowthStage(int id, [FromBody] Garden garden)
    {
        if (id != garden.ID)
        {
            return BadRequest("Garden ID is mismatch");
        }

        var existingGarden = await _context.Garden.FindAsync(id);

        if (existingGarden != null)
        {
            existingGarden.GrowthStage = garden.GrowthStage;

            _context.Garden.Update(existingGarden);
        }

        await _context.SaveChangesAsync();
        
        await _hubContext.Clients.Group(existingGarden.UserID.ToString()).SendAsync("ReceiveGardenUpdate", existingGarden);
        
        return NoContent();
    }
}