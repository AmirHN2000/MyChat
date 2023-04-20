using Client.App.Services.Contracts;

namespace Client.App.Services.Implementations;

public class ClientSideAuthTokenProvider : IAuthTokenProvider
{
    public Task<string> GetAccessTokenAsync()
    {
        return Task.FromResult(Preferences.Get("access_token", null));
    }
}