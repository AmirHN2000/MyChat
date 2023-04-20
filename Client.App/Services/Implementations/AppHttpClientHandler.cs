using System.Net;
using System.Net.Http.Headers;
using System.Security.Authentication;
using Client.App.Services.Contracts;

namespace Client.App.Services.Implementations;

public class AppHttpClientHandler : HttpClientHandler
{
    private IAuthTokenProvider _tokenProvider;

    public AppHttpClientHandler(IAuthTokenProvider authTokenProvider)
    {
        _tokenProvider = authTokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization is null)
        {
            var access_token = await _tokenProvider.GetAccessTokenAsync();
            if (access_token is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            }
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is HttpStatusCode.Unauthorized)
        {
            throw new AuthenticationException("شما لاگین نیستید.");
        }

        response.EnsureSuccessStatusCode();

        return response;
    }
}