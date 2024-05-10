using ManageActivities.Interfaces;

namespace ManageActivities.Entity.Model;

public record User : IEntity
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public List<Activity> Activities { get; set; } = [];
    public List<Category> Categories { get; set; } = [];
    public required Guid Id { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; set; }
}