using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyGarden.Common;
using Task = StudyGarden.Entities.Task;

namespace StudyGarden.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("GetCurrentUserTasks")]
    public async Task<IActionResult> GetTasks(int userId)
    {
        var tasks = await _context.Task
            .Where(t => t.UserID == userId)
            .ToListAsync();
        return Ok(tasks);
    }
    
    [HttpGet("GetTaskByID/{id}")]
    public async Task<IActionResult> GetTaskByID(int userId, int id)
    {
        var task = await _context.Task
            .FirstOrDefaultAsync(c => c.ID == id && c.UserID == userId);
        
        if (task == null)
        {
            return NotFound(new { message = "Task not found or does not belong to the user." });
        }

        return Ok(task);
    }
    
    [HttpPost("AddNewTask")]
    public async Task<IActionResult> CreateTask(int userId, [FromBody] Task task)
    {
        var newTask = new Task
        {
            UserID = userId,
            Title = task.Title,
            Description = task.Description,
            CategoryID = task.CategoryID,
            PlantID = task.PlantID,
            Priority = task.Priority
        };

        _context.Task.Add(newTask);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Task created successfully" });
    }
    
    [HttpPut("UpdateTask")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] Task task)
    {
        if (id != task.ID)
        {
            return BadRequest("Task ID mismatch.");
        }

        var existingTask = await _context.Task.FindAsync(id);
        if (existingTask == null)
        {
            return NotFound();
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.CategoryID = task.CategoryID;
        existingTask.PlantID = task.PlantID;
        existingTask.DueDate = task.DueDate;
        existingTask.Status = task.Status;
        existingTask.Priority = task.Priority;

        _context.Task.Update(existingTask);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}