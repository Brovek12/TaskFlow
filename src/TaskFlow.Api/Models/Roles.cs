namespace TaskFlow.Api.Models;

public static class Roles
{
    public const string User = "User";
    public const string Admin = "Admin";

    // Semua role yang valid
    public static readonly string[] All = { User, Admin };
}