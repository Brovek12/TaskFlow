using System.Net.Http.Headers;
using System.Net.Http.Json;
using TaskFlow.Client.Models;

namespace TaskFlow.Client.Services;

public class TaskService(HttpClient http, TokenStore store)
{
    private async Task<HttpRequestMessage> WithAuthAsync(HttpMethod method, string url)
    {
        var req = new HttpRequestMessage(method, url);
        var token = await store.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return req;
    }

    public async Task<List<TaskResponse>> GetAllAsync()
    {
        var req = await WithAuthAsync(HttpMethod.Get, "api/tasks");
        var res = await http.SendAsync(req);
        if (!res.IsSuccessStatusCode) return new();
        return await res.Content.ReadFromJsonAsync<List<TaskResponse>>() ?? new();
    }

    public async Task<bool> CreateAsync(string title, string? description)
    {
        var req = await WithAuthAsync(HttpMethod.Post, "api/tasks");
        req.Content = JsonContent.Create(new CreateTaskRequest(title, description));
        var res = await http.SendAsync(req);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(int id, string title, string? description, bool isDone)
    {
        var req = await WithAuthAsync(HttpMethod.Put, $"api/tasks/{id}");
        req.Content = JsonContent.Create(new UpdateTaskRequest(title, description, isDone));
        var res = await http.SendAsync(req);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var req = await WithAuthAsync(HttpMethod.Delete, $"api/tasks/{id}");
        var res = await http.SendAsync(req);
        return res.IsSuccessStatusCode;
    }
}