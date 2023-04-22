using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Client.App.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.App.Services.Implementations;

public class AppAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAuthTokenProvider _tokenProvider;

    public AppAuthenticationStateProvider(IAuthTokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    public async Task RaiseAuthenticationStateHasChanged()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(await GetAuthenticationStateAsync()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var access_token = await _tokenProvider.GetAccessTokenAsync();

        if (string.IsNullOrWhiteSpace(access_token))
        {
            return NotSignedIn();
        }

        var identity = new ClaimsIdentity(claims: ParseTokenClaims(access_token), authenticationType: "Bearer",
            nameType: "name", roleType: "role");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task<bool> IsUserAuthenticatedAsync()
    {
        return (await GetAuthenticationStateAsync()).User.Identity?.IsAuthenticated == true;
    }

    AuthenticationState NotSignedIn()
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    private IEnumerable<Claim> ParseTokenClaims(string access_token)
    {
        var claims = new JwtSecurityTokenHandler().ReadJwtToken(access_token).Claims.ToArray();
        return claims;
    }
}