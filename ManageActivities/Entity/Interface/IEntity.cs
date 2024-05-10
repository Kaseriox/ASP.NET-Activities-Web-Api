namespace ManageActivities.Interfaces;

public interface IEntity
{
    public Guid Id { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; set; }
}