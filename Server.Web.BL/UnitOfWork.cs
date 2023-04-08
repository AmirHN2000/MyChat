using MyChat.Server.BL.Repository;
using MyChat.Server.DB;
using MyChat.Server.DB.Entities;
using MyChat.Shared.Interface;

namespace Server.Web.BL;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext Context;

    // private GenericRepository<test> _testRepository;
    // public GenericRepository<test> TestRepository
    // {
    //     get
    //     {
    //         if (this._testRepository == null)
    //         {
    //             this._testRepository = new GenericRepository<test>(Context);
    //         }
    //
    //         return _testRepository;
    //     }
    // }
    
    
    
    

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