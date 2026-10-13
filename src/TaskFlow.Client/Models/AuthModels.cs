namespace TaskFlow.Client.Models;

public record RegisterRequest(string Email, string Password);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token, string Email);
public record TaskResponse(int Id, string Title, string? Description, bool IsDone, DateTime CreatedAt);
public record CreateTaskRequest(string Title, string? Description);
public record UpdateTaskRequest(string Title, string? Description, bool IsDone);