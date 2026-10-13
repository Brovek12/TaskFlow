namespace TaskFlow.Api.DTOs;

public record CreateTaskDto(string Title, string? Description);
public record UpdateTaskDto(string Title, string? Description, bool IsDone);
public record TaskResponse(int Id, string Title, string? Description, bool IsDone, DateTime CreatedAt);