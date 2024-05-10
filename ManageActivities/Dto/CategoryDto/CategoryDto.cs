using ManageActivities.Dto.ActivityDto;
using ManageActivities.Entity.Model;

namespace ManageActivities.Dto.CategoryDto;

public record CategoryDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime Created { get; set; }
    public required DateTime Updated { get; set; }
}

public record ExpandedCategoryDto : CategoryDto
{
    public List<ActivityDto.ActivityDto> Activities { get; set; }
}

public static class CategoryDtoExtensions
{
    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Created = category.Created,
            Updated = category.Updated
        };
    }

    public static ExpandedCategoryDto ToExpandedCategoryDto(this Category category)
    {
        return new ExpandedCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Created = category.Created,
            Updated = category.Updated,
            Activities = category.Activities.Select(activity => activity.ToDto()).ToList()
        };
    }
}