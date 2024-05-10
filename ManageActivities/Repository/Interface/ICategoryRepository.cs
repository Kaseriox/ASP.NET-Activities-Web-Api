using ManageActivities.Entity.Model;

namespace ManageActivities.Repository.Interface;

public interface ICategoryRepository : IGenericSqlRepository<Category>
{
    public Task<bool> UserOwnsThisCategory(Guid categoryId, Guid userId);
    Task<List<Category>> GetCategoriesByUserId(Guid id);
    Task<List<Category>> GetExpandedCategoriesByUserId(Guid id);
}