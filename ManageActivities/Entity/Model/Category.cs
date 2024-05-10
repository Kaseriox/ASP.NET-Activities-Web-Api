using ManageActivities.Interfaces;

namespace ManageActivities.Entity.Model;

public record Category : IEntity
{
    public required string Name { get; set; }
    public User User { get; set; } = null!;
    public required Guid UserId { get; set; }
    public List<Activity> Activities { get; set; } = [];
    public required Guid Id { get; init; }
    public required DateTime Created { get; init; }
    public required DateTime Updated { get; set; }
}