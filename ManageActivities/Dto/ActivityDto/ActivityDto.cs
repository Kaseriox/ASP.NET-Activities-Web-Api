using Activity = ManageActivities.Entity.Model.Activity;

namespace ManageActivities.Dto.ActivityDto;

public record ActivityDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime Until { get; set; }
    public Guid? CategoryId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}

public static class ActivityDtoExtensions
{
    public static ActivityDto ToDto(this Activity activity)
    {
        return new ActivityDto
        {
            Id = activity.Id,
            Name = activity.Name,
            Until = activity.Until,
            Created = activity.Created,
            Updated = activity.Updated,
            CategoryId = activity.CategoryId
        };
    }
}