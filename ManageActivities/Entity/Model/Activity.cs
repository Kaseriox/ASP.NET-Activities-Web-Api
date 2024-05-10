using ManageActivities.Interfaces;

namespace ManageActivities.Entity.Model;

public record Activity : IEntity
{
    public required string Name { get; set; }
    public required DateTime Until { get; set; }
    public User User { get; init; } = null!;
    public required Guid UserId { get; set; }

    public Category Category { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public required Guid Id { get; init; }

    public DateTime Created { get; init; }
    public DateTime Updated { get; set; }
}