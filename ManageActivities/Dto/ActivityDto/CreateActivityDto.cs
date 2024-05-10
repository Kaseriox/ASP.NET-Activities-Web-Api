using System.ComponentModel.DataAnnotations;

namespace ManageActivities.Dto.ActivityDto;

public record CreateActivityDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Activity Name Has To Be At Least 3 Characters Long")]
    [MaxLength(30, ErrorMessage = "Activity Name Cannot Be Longer Than 30 Characters")]
    public required string Name { get; set; }

    [Required] public required DateTime Until { get; set; }

    [Required] public Guid CategoryId { get; set; }
}