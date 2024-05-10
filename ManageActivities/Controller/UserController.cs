using ManageActivities.Dto.UserDto;
using ManageActivities.Entity.Model;
using ManageActivities.Repository.Interface;
using ManageActivities.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ManageActivities.Controller;

[ApiController]
[Authorize]
[Route("/user")]
public class UserController(IUserRepository userRepository, IMemoryCache memoryCache)
    : AuthenticatedControllerBase(userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetUserInformation()
    {
        Guid userId = await VerifyUser();
        if (userId == Guid.Empty)
        {
            return Unauthorized();
        }

        User? cachedUser = memoryCache.Get<User>("user" + userId);
        if (cachedUser is not null)
        {
            return Ok(cachedUser.ToDto());
        }

        User? user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return Unauthorized();
        }

        memoryCache.Set("user" + user.Id, user);
        return Ok(user.ToDto());
    }
}