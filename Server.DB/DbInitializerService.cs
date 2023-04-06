using Microsoft.EntityFrameworkCore;

namespace MyChat.Server.DB;

public interface IDbInitializerService
{
    /// <summary>
    /// Applies any pending migrations for the context to the database.
    /// Will create the database if it does not already exist.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Adds some default values to the Db
    /// </summary>
    void SeedData();
}

public class DbInitializerService : IDbInitializerService
{
    private readonly AppDbContext _dbContext;


    public DbInitializerService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Initialize()
    {
        _dbContext.Database.Migrate();
    }

    public void SeedData()
    {
    }
}