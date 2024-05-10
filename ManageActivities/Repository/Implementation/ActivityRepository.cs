using ManageActivities.Context;
using ManageActivities.Entity.Model;
using ManageActivities.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ManageActivities.Repository.Implementation;

public class ActivityRepository(DatabaseContext context) : GenericSqlRepository<Activity>(context), IActivityRepository
{
    private readonly DatabaseContext _context = context;

    public async Task<List<Activity>> GetActivitiesByUserId(Guid id)
    {
        return await _context.Set<Activity>().AsNoTracking().Where(activity => activity.User.Id == id).ToListAsync();
    }

    public async Task<bool> UserOwnThisActivity(Guid activityId, Guid userId)
    {
        Activity? activity = await _context.Set<Activity>().AsNoTracking()
            .FirstOrDefaultAsync(activity => activity.UserId == userId && activity.Id == activityId);
        return activity is not null;
    }
}