using ManageActivities.Context;
using ManageActivities.Entity.Model;
using ManageActivities.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ManageActivities.Repository.Implementation;

public class CategoryRepository(DatabaseContext context) : GenericSqlRepository<Category>(context), ICategoryRepository
{
    private readonly DatabaseContext _context = context;

    public async Task<bool> UserOwnsThisCategory(Guid categoryId, Guid userId)
    {
        Category? foundCategory = await _context.Categories.Include(category => category.User)
            .FirstOrDefaultAsync(category => category.Id == categoryId && category.UserId == userId);
        return foundCategory != null;
    }

    public async Task<List<Category>> GetCategoriesByUserId(Guid id)
    {
        return await _context.Set<Category>().AsNoTracking().Where(category => category.UserId == id).ToListAsync();
    }

    public async Task<List<Category>> GetExpandedCategoriesByUserId(Guid id)
    {
        return await _context.Set<Category>().AsNoTracking().Include(category => category.Activities)
            .Where(category => category.UserId == id).ToListAsync();
    }
}