using System.Net.Http.Headers;
using System.Net.Http.Json;
using TaskFlow.Client.Models;

namespace TaskFlow.Client.Services;

public class AdminService(HttpClient http, TokenStore store)
{
    private async Task<HttpRequestMessage> WithAuthAsync(HttpMethod method, string url)
    {
        var req = new HttpRequestMessage(method, url);
        var token = await store.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return req;
    }

    public async Task<List<AdminUserResponse>> GetUsersAsync()
    {
        var req = await WithAuthAsync(HttpMethod.Get, "api/admin/users");
        var res = await http.SendAsync(req);
        if (!res.IsSuccessStatusCode) return new();
        return await res.Content.ReadFromJsonAsync<List<AdminUserResponse>>() ?? new();
    }

    public async Task<List<AdminTaskResponse>> GetTasksAsync()
    {
        var req = await WithAuthAsync(HttpMethod.Get, "api/admin/tasks");
        var res = await http.SendAsync(req);
        if (!res.IsSuccessStatusCode) return new();
        return await res.Content.ReadFromJsonAsync<List<AdminTaskResponse>>() ?? new();
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var req = await WithAuthAsync(HttpMethod.Delete, $"api/admin/users/{id}");
        var res = await http.SendAsync(req);
        return res.IsSuccessStatusCode;
    }
}