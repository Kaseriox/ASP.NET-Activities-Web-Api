using System.ComponentModel.DataAnnotations;

namespace ManageActivities.Dto.AccountDto;

public record LoginUserDto
{
    [Required] public required string Username { get; set; }

    [Required] public required string Password { get; set; }
}