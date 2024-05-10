using ManageActivities.Entity.Model;

namespace ManageActivities.Repository.Interface;

public interface IUserRepository : IGenericSqlRepository<User>
{
    public Task<User?> GetUserByUsername(string username);
}