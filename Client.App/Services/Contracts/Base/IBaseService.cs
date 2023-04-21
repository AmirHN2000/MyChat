namespace Client.App.Services.Contracts.Base;

public interface IBaseService
{
    Task<T?> Get<T>(string url, bool showMessage = false);
    Task<TResponse?> Post<TRequest, TResponse>(string url, TRequest model, bool showMessage = false);
}