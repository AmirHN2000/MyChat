using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyChat.Server.DB.Base;
using MyChat.Server.DB.Entities.Chats;
using MyChat.Server.DB.Entities.Role;
using MyChat.Server.DB.Entities.Users;

namespace MyChat.Server.DB;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        CallRemoveQueryMethod(builder);
        
        builder.Entity<ChatGroup>().HasOne(x => x.Owner)
            .WithMany(x => x.ChatGroupOwners)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        
        base.OnModelCreating(builder);
    }

    #region DbSets

    #region Chart

    public DbSet<ChatGroup> ChatGroups { get; set; }
    
    public DbSet<ChatGroupUser> ChatGroupUsers { get; set; }
    
    public DbSet<Chat> Chats { get; set; }
    
    public DbSet<ChatFile> ChatFiles { get; set; }

    #endregion

    #endregion
    

    #region Global Query Filter For Dynamic Delete

    public void SetGlobalRemoveQuery<T>(ModelBuilder builder) where T : BaseEntity
    {
        builder.Entity<T>().AppendQueryFilter(e => !e.IsDeleted);
    }
        
    static readonly MethodInfo SetGlobalRemoveQueryMethod = typeof(AppDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalRemoveQuery");
   
    private void CallRemoveQueryMethod(ModelBuilder modelBuilder)
    {
        var types = GetClrTypes(modelBuilder,typeof(BaseEntity));
        foreach (var type in types)
        {
            var method = SetGlobalRemoveQueryMethod.MakeGenericMethod(type);
            method.Invoke(this, new object[] { modelBuilder });
        }
    }
    
    private IEnumerable<IMutableEntityType> GetEntities(ModelBuilder modelBuilder)
    {
        var entities = modelBuilder.Model.GetEntityTypes();
        return entities;
    }
    
    private List<Type> GetClrTypes(ModelBuilder modelBuilder, Type type)
    {
        var entities = GetEntities(modelBuilder);
        var types =entities
            .Select(x => x.ClrType)
            .Where(x=>x.BaseType==type || x.GetInterfaces().Contains(type))
            .ToList();

        return types;
    }

    #endregion
}

