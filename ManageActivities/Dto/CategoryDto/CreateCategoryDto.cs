using System.ComponentModel.DataAnnotations;

namespace ManageActivities.Dto.CategoryDto;

public record CreateCategoryDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Category Name Has To Be At Least 3 Characters Long")]
    public required string Name { get; set; }
}