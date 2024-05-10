using ManageActivities.Dto.ActivityDto;
using ManageActivities.Entity.Model;
using ManageActivities.Repository.Interface;
using ManageActivities.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ManageActivities.Controller;

[ApiController]
[Authorize]
[Route("/activity/")]
public class ActivityController(
    IActivityRepository activityRepository,
    ICategoryRepository categoryRepository,
    IUserRepository userRepository,
    IMemoryCache memoryCache)
    : AuthenticatedControllerBase(userRepository)
{
    private const string CacheKey = "activities";
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet]
    public async Task<ActionResult<List<ActivityDto>>> GetUserActivities()
    {
        Guid userId = await VerifyUser();
        if (userId == Guid.Empty)
        {
            return Unauthorized();
        }

        IEnumerable<ActivityDto>? cachedActivityDtos = memoryCache.Get<IEnumerable<ActivityDto>>(CacheKey + userId);
        if (cachedActivityDtos is not null)
        {
            return Ok(cachedActivityDtos);
        }

        List<ActivityDto> activities = (await activityRepository.GetActivitiesByUserId(userId))
            .Select(activity => activity.ToDto()).ToList();
        memoryCache.Set(CacheKey + userId, activities);
        return Ok(activities);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTask([FromBody] CreateActivityDto createActivityDto)
    {
        Guid userGuid = await VerifyUser();
        if (userGuid == Guid.Empty)
        {
            return Unauthorized();
        }

        bool categoryExists = await categoryRepository.UserOwnsThisCategory(createActivityDto.CategoryId, userGuid);
        if (!categoryExists)
        {
            return Unauthorized();
        }

        Activity activity = new Activity
        {
            Id = Guid.NewGuid(),
            Name = createActivityDto.Name,
            Until = createActivityDto.Until,
            Created = DateTime.Now,
            Updated = DateTime.Now,
            UserId = userGuid,
            CategoryId = createActivityDto.CategoryId
        };


        await activityRepository.CreateEntityAsync(activity);
        memoryCache.Remove(CacheKey + userGuid);
        return Ok();
    }

    [HttpPut("{activityId:guid}")]
    public async Task<ActionResult> UpdateActivity([FromBody] UpdateActivityDto updateActivityDto,
        [FromRoute] Guid activityId)
    {
        Guid userId = await VerifyUser();
        if (userId == Guid.Empty)
        {
            return Unauthorized();
        }

        bool userOwnsThisActivity = await activityRepository.UserOwnThisActivity(activityId, userId);
        if (!userOwnsThisActivity)
        {
            return Unauthorized();
        }

        Activity? activity = await activityRepository.GetByIdAsync(activityId);
        if (activity is null)
        {
            return BadRequest();
        }

        activity.Name = updateActivityDto.Name ?? activity.Name;
        activity.CategoryId = updateActivityDto.CategoryId ?? activity.CategoryId;
        activity.Until = updateActivityDto.Until ?? activity.Until;
        activity.Updated = DateTime.Now;
        await activityRepository.UpdateEntityAsync(activity);
        memoryCache.Remove(CacheKey + userId);
        return Ok();
    }
}