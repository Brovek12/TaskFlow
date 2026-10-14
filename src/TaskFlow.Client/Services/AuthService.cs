using System.Net.Http.Json;
using TaskFlow.Client.Models;

namespace TaskFlow.Client.Services;

public class AuthService(HttpClient http, TokenStore store)
{
    public async Task<(bool ok, string? error)> RegisterAsync(string email, string password)
    {
        var res = await http.PostAsJsonAsync("api/auth/register", new RegisterRequest(email, password));
        if (!res.IsSuccessStatusCode)
            return (false, await res.Content.ReadAsStringAsync());
        var data = await res.Content.ReadFromJsonAsync<AuthResponse>();
        await store.SetAsync(data!.Token, data.Email, data.Role);
        return (true, null);
    }

    public async Task<(bool ok, string? error)> LoginAsync(string email, string password)
    {
        var res = await http.PostAsJsonAsync("api/auth/login", new LoginRequest(email, password));
        if (!res.IsSuccessStatusCode)
            return (false, "Email atau password salah");
        var data = await res.Content.ReadFromJsonAsync<AuthResponse>();
        await store.SetAsync(data!.Token, data.Email, data.Role);
        return (true, null);
    }

    public async Task LogoutAsync() => await store.ClearAsync();
    public async Task<bool> IsLoggedInAsync() => !string.IsNullOrEmpty(await store.GetTokenAsync());
    public async Task<string?> GetEmailAsync() => await store.GetEmailAsync();
    public async Task<string?> GetRoleAsync() => await store.GetRoleAsync();
    public async Task<bool> IsAdminAsync() => (await store.GetRoleAsync()) == "Admin";
}