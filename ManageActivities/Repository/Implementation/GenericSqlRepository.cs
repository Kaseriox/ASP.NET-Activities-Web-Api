using ManageActivities.Context;
using ManageActivities.Interfaces;
using ManageActivities.Repository.Interface;
using ManageActivities.Repository.Utils;
using Microsoft.EntityFrameworkCore;

namespace ManageActivities.Repository.Implementation;

public class GenericSqlRepository<T>(DatabaseContext context) : IGenericSqlRepository<T> where T : class, IEntity
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }
        catch (Exception e)
        {
            throw new RepositoryException(ERepositoryExceptionReason.FindingAll, e.StackTrace);
        }
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        try
        {
            return await context.Set<T>().AsNoTracking().SingleOrDefaultAsync(entity => entity.Id == id);
        }
        catch (Exception e)
        {
            throw new RepositoryException(ERepositoryExceptionReason.FindingOne, e.StackTrace);
        }
    }

    public async Task CreateEntityAsync(T entity)
    {
        try
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new RepositoryException(ERepositoryExceptionReason.Creating, e.StackTrace);
        }
    }

    public async Task UpdateEntityAsync(T entity)
    {
        try
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new RepositoryException(ERepositoryExceptionReason.Updating, e.StackTrace);
        }
    }

    public async Task DeleteEntityAsync(Guid id)
    {
        try
        {
            await context.Set<T>().AsNoTracking().Where(entity => entity.Id == id).ExecuteDeleteAsync();
        }
        catch (Exception e)
        {
            throw new RepositoryException(ERepositoryExceptionReason.Deleting, e.StackTrace);
        }
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        try
        {
            return await context.Set<T>().AsNoTracking().AnyAsync(entity => entity.Id == id);
        }
        catch (Exception e)
        {
            throw new RepositoryException(ERepositoryExceptionReason.ExistsById, e.StackTrace);
        }
    }
}