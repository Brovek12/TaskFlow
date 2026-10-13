using Microsoft.JSInterop;

namespace TaskFlow.Client.Services;

public class TokenStore(IJSRuntime js)
{
    private const string Key = "taskflow_token";
    private const string EmailKey = "taskflow_email";

    public async Task SetAsync(string token, string email)
    {
        await js.InvokeVoidAsync("localStorage.setItem", Key, token);
        await js.InvokeVoidAsync("localStorage.setItem", EmailKey, email);
    }

    public async Task<string?> GetTokenAsync()
        => await js.InvokeAsync<string?>("localStorage.getItem", Key);

    public async Task<string?> GetEmailAsync()
        => await js.InvokeAsync<string?>("localStorage.getItem", EmailKey);

    public async Task ClearAsync()
    {
        await js.InvokeVoidAsync("localStorage.removeItem", Key);
        await js.InvokeVoidAsync("localStorage.removeItem", EmailKey);
    }
}