using ManageActivities.Entity.Model;

namespace ManageActivities.Repository.Interface;

public interface IActivityRepository : IGenericSqlRepository<Activity>
{
    Task<List<Activity>> GetActivitiesByUserId(Guid id);
    Task<bool> UserOwnThisActivity(Guid activityId, Guid userId);
}