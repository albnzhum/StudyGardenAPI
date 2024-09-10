using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyGarden.Common;
using StudyGarden.Entities;

namespace StudyGarden.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("GetCurrentUserCategories")]
    public async Task<IActionResult> GetCategories()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var categories = await _context.Category
            .Where(c => c.UserID == userId)
            .ToListAsync();
        return Ok(categories);
    }

    [HttpGet("GetCategoryByID/{id}")]
    public async Task<IActionResult> GetCategoryByID(int id)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var category = await _context.Category
            .FirstOrDefaultAsync(c => c.ID == id && c.UserID == userId);
        
        if (category == null)
        {
            return NotFound(new { message = "Category not found or does not belong to the user." });
        }

        return Ok(category);
    }
    
    [HttpPost("AddNewCategory")]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var newCategory = new Category
        {
            UserID = userId,
            Title = category.Title,
            PlantTypeID = category.PlantTypeID,
        };

        _context.Category.Add(newCategory);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category created successfully" });
    }
    
    [HttpPut("UpdateCategory/{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
    {
        if (id != category.ID)
        {
            return BadRequest("Category ID mismatch.");
        }

        var existingCategory = await _context.Category.FindAsync(id);
        if (existingCategory == null)
        {
            return NotFound();
        }

        existingCategory.Title = category.Title;
        existingCategory.PlantTypeID = category.PlantTypeID;

        _context.Category.Update(existingCategory);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Category.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Category.Remove(category);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category deleted successfully" });
    }
}