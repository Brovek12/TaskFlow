using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskFlow.Api.Data;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController(AppDbContext db) : ControllerBase
{
    private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> GetAll()
    {
        var tasks = await db.Tasks
            .Where(t => t.UserId == UserId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TaskResponse(t.Id, t.Title, t.Description, t.IsDone, t.CreatedAt))
            .ToListAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponse>> GetById(int id)
    {
        var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == UserId);
        if (task is null) return NotFound();
        return Ok(new TaskResponse(task.Id, task.Title, task.Description, task.IsDone, task.CreatedAt));
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> Create(CreateTaskDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest("Title wajib diisi");

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = UserId
        };
        db.Tasks.Add(task);
        await db.SaveChangesAsync();
        return Ok(new TaskResponse(task.Id, task.Title, task.Description, task.IsDone, task.CreatedAt));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
    {
        var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == UserId);
        if (task is null) return NotFound();

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.IsDone = dto.IsDone;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == UserId);
        if (task is null) return NotFound();

        db.Tasks.Remove(task);
        await db.SaveChangesAsync();
        return NoContent();
    }
}