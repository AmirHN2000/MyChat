namespace MyChat.Shared.Interface;

public interface IUnitOfWork : IDisposable
{
    public int Save();
    Task<int> SaveAsync();
}