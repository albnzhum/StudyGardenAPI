using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyGarden.Common;
using StudyGarden.Entities;

namespace StudyGarden.Controllers;

[ApiController]
[Route("[controller]")]
public class GardenController : ControllerBase
{
    private readonly AppDbContext _context;

    public GardenController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize(Policy = "IsOwner")]
    [HttpGet("ViewFriendGarden/{id}")]
    public async Task<IActionResult> ViewFriendGarden(int id)
    {
        var garden = await _context.Garden
            .Where(g => g.UserID == id)
            .ToListAsync();

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
        return NoContent();
    }
    
    //TODO: настройка signalR для реал тайм обновления, добавление хаба + подписка на изменения
}