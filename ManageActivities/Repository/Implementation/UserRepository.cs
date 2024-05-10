using ManageActivities.Context;
using ManageActivities.Entity.Model;
using ManageActivities.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ManageActivities.Repository.Implementation;

public class UserRepository(DatabaseContext context) : GenericSqlRepository<User>(context), IUserRepository
{
    private readonly DatabaseContext _context = context;

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _context.Set<User>().SingleOrDefaultAsync(user => user.Username == username);
    }
}