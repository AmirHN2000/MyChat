using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyChat.Server.DB;
using MyChat.Server.DB.Base;

namespace MyChat.Server.BL.Repository;

public class GenericRepository<TEntity> where TEntity : BaseEntity
{
    protected AppDbContext context;
    protected DbSet<TEntity> dbSet;

    public GenericRepository(AppDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public virtual TEntity GetByID(object id)
    {
        return dbSet.Find(id);
    }

    public virtual void Insert(TEntity entity)
    {
        entity.CreationDate = DateTime.Now;
        dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        entityToDelete.IsDeleted = true;
        entityToDelete.DeletedDate = DateTime.Now;
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        entityToUpdate.EditionDate = DateTime.Now;
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}