namespace ManageActivities.Repository.Interface;

public interface IGenericSqlRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task CreateEntityAsync(T entity);
    Task UpdateEntityAsync(T entity);
    Task DeleteEntityAsync(Guid id);
    Task<bool> ExistsByIdAsync(Guid id);
}