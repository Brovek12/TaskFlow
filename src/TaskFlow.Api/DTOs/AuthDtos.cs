namespace TaskFlow.Api.DTOs;

public record RegisterDto(string Email, string Password);
public record LoginDto(string Email, string Password);
public record AuthResponse(string Token, string Email, string Role);