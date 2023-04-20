namespace Client.App.Services.Contracts.Base;

public interface IBaseService
{
    Task<T?> Get<T>(string url, bool showMessage = true);
}