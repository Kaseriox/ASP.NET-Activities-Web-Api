using System.ComponentModel.DataAnnotations;

namespace ManageActivities.Dto.ActivityDto;

public record UpdateActivityDto
{
    [MinLength(3, ErrorMessage = "Activity Name Has To Be At Least 3 Characters Long")]
    [MaxLength(30, ErrorMessage = "Activity Name Cannot Be Longer Than 30 Characters")]
    public string? Name { get; set; }

    public DateTime? Until { get; set; }
    public Guid? CategoryId { get; set; }
}