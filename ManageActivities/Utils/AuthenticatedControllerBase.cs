using System.Security.Claims;
using ManageActivities.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ManageActivities.Utils;

public class AuthenticatedControllerBase(IUserRepository userRepository) : ControllerBase
{
    protected async Task<Guid> VerifyUser()
    {
        ClaimsPrincipal bearer = HttpContext.User;
        Guid userGuid;
        string? userId = bearer.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null)
        {
            return Guid.Empty;
        }

        if (!Guid.TryParse(userId, out userGuid))
        {
            return Guid.Empty;
        }

        bool userExists = await userRepository.ExistsByIdAsync(userGuid);
        if (userExists == false)
        {
            return Guid.Empty;
        }

        return userGuid;
    }
}