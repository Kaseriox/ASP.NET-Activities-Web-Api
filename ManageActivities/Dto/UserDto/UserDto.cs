using ManageActivities.Entity.Model;

namespace ManageActivities.Dto.UserDto;

public record UserDto
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required DateTime Created { get; set; }
    public required DateTime Updated { get; set; }
}

public static class UserDtoExtensions
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Created = user.Created,
            Updated = user.Updated
        };
    }
}