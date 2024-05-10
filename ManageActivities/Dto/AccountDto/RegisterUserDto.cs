using System.ComponentModel.DataAnnotations;

namespace ManageActivities.Dto.AccountDto;

public record RegisterUserDto
{
    [Required(ErrorMessage = "Username Is Required")]
    [MinLength(3, ErrorMessage = "Username Has To Be At Least 3 Characters Long")]
    [MaxLength(20, ErrorMessage = "Username Cannot Be Longer Than 20 Characters")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Password Is Required")]
    [MinLength(6, ErrorMessage = "Password Has To Be At Least 6 Characters Long")]
    [MaxLength(30, ErrorMessage = "Password Cannot Be Longer Than 30 Characters")]
    public required string Password { get; set; }
}