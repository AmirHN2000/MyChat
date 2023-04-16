using MyChat.Server.BL.Repository;
using MyChat.Server.DB;
using MyChat.Server.DB.Entities.Chats;

namespace Server.Web.BL;

public class UnitOfWork
{
    private readonly AppDbContext Context;

    private GenericRepository<ChatGroup> _chatGroupRepository;
    private GenericRepository<ChatGroupUser> _chatGroupUserRepository;
    private GenericRepository<Chat> _chatRepository;
    private GenericRepository<ChatFile> _chatFileRepository;
    public GenericRepository<ChatGroup> ChatGroupRepository
    {
        get
        {
            if (this._chatGroupRepository == null)
            {
                this._chatGroupRepository = new GenericRepository<ChatGroup>(Context);
            }
    
            return _chatGroupRepository;
        }
    }
    
    public GenericRepository<ChatGroupUser> ChatGroupUserRepository
    {
        get
        {
            if (this._chatGroupUserRepository == null)
            {
                this._chatGroupUserRepository = new GenericRepository<ChatGroupUser>(Context);
            }
    
            return _chatGroupUserRepository;
        }
    }
    
    public GenericRepository<Chat> ChatRepository
    {
        get
        {
            if (this._chatRepository == null)
            {
                this._chatRepository = new GenericRepository<Chat>(Context);
            }
    
            return _chatRepository;
        }
    }
    
    public GenericRepository<ChatFile> ChatFileRepository
    {
        get
        {
            if (this._chatFileRepository == null)
            {
                this._chatFileRepository = new GenericRepository<ChatFile>(Context);
            }
    
            return _chatFileRepository;
        }
    }
    
    

    public UnitOfWork(AppDbContext appDbContext)
    {
        Context = appDbContext;
    }

    public int Save()
    {
        return Context.SaveChanges();
    }
    
    public async Task<int> SaveAsync()
    {
        return await Context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}