using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyGarden.Common;
using StudyGarden.Entities;

namespace StudyGarden.Controllers;

[ApiController]
[Route("[controller]")]
public class PlantController : ControllerBase
{
    private readonly AppDbContext _context;

    public PlantController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("GetAllPlants")]
    public async Task<IActionResult> GetAllPlants()
    {
        var plant = await _context.Plant
            .ToListAsync();
        return Ok(plant);
    }

    [HttpGet("GetAllUnlockedPlants")]
    public async Task<IActionResult> GetAllUnlockedPlants()
    {
        var plant = await _context.Plant
            .Where(p => p.IsUnlocked)
            .ToListAsync();
        return Ok(plant);
    }
    
    [HttpGet("GetPlantByID/{id}")]
    public async Task<IActionResult> GetPlantByID(int id)
    {
        var plant = await _context.Plant
            .FirstOrDefaultAsync(c => c.ID == id);
        
        if (plant == null)
        {
            return NotFound(new { message = "Plant not found or does not belong to the user." });
        }

        return Ok(plant);
    }

    [HttpGet("GetPlantsByType")]
    public async Task<IActionResult> GetPlantsByType(int plantTypeId)
    {
        var plant = await _context.Plant
            .Where(p => p.PlantTypeID == plantTypeId)
            .ToListAsync();
        return Ok(plant);
    }

    [HttpPut("UnlockPlant/{id}")]
    public async Task<IActionResult> UnlockPlant(int id, [FromBody] Plant plant)
    {
        if (id != plant.ID)
        {
            return BadRequest("Task ID mismatch.");
        }

        var existingPlant = await _context.Plant.FindAsync(id);
        if (existingPlant == null)
        {
            return NotFound();
        }

        existingPlant.IsUnlocked = plant.IsUnlocked;

        _context.Plant.Update(existingPlant);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}