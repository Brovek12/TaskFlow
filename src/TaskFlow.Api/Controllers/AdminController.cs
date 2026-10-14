using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Admin)]   // ← hanya Admin
public class AdminController(AppDbContext db) : ControllerBase
{
    // GET /api/admin/users
    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
    {
        var users = await db.Users
            .OrderBy(u => u.Id)
            .Select(u => new UserResponse(
                u.Id,
                u.Email,
                u.Role,
                u.CreatedAt,
                u.Tasks.Count
            ))
            .ToListAsync();

        return Ok(users);
    }

    // GET /api/admin/tasks
    [HttpGet("tasks")]
    public async Task<ActionResult<IEnumerable<AdminTaskResponse>>> GetAllTasks()
    {
        var tasks = await db.Tasks
            .Include(t => t.User)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new AdminTaskResponse(
                t.Id,
                t.Title,
                t.Description,
                t.IsDone,
                t.CreatedAt,
                t.User!.Email
            ))
            .ToListAsync();

        return Ok(tasks);
    }

    // DELETE /api/admin/users/{id}
    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return NotFound();

        // Cegah admin hapus dirinya sendiri
        var currentUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        if (user.Id == currentUserId)
            return BadRequest("Tidak bisa menghapus akun sendiri");

        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return NoContent();
    }
}