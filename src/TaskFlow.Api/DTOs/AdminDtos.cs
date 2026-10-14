namespace TaskFlow.Api.DTOs;

public record UserResponse(int Id, string Email, string Role, DateTime CreatedAt, int TaskCount);
public record AdminTaskResponse(
    int Id,
    string Title,
    string? Description,
    bool IsDone,
    DateTime CreatedAt,
    string OwnerEmail
);