using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

namespace TaskFlow.Tests;

public class AuthTests
{
    private AppDbContext GetDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(opts);
    }

    [Fact]
    public async Task Can_Create_User()
    {
        var db = GetDb();
        db.Users.Add(new User { Email = "a@b.com", PasswordHash = "x" });
        await db.SaveChangesAsync();
        Assert.Equal(1, await db.Users.CountAsync());
    }

    [Fact]
    public void Bcrypt_Hash_And_Verify_Works()
    {
        var hash = BCrypt.Net.BCrypt.HashPassword("secret");
        Assert.True(BCrypt.Net.BCrypt.Verify("secret", hash));
        Assert.False(BCrypt.Net.BCrypt.Verify("wrong", hash));
    }

    [Fact]
    public async Task Task_Is_Scoped_To_User()
    {
        var db = GetDb();
        db.Tasks.Add(new TaskItem { Title = "A", UserId = 1 });
        db.Tasks.Add(new TaskItem { Title = "B", UserId = 2 });
        await db.SaveChangesAsync();

        var user1Tasks = await db.Tasks.Where(t => t.UserId == 1).ToListAsync();
        Assert.Single(user1Tasks);
    }
}