using ManageActivities.Dto.AccountDto;
using ManageActivities.Entity.Model;
using ManageActivities.Repository.Interface;
using ManageActivities.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ManageActivities.Controller;

[ApiController]
public class AccountController(IUserRepository userRepository, IConfiguration configuration) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> CreateUser([FromBody] RegisterUserDto registerUserDto)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            Username = registerUserDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
            Created = DateTime.Now,
            Updated = DateTime.Now
        };
        await userRepository.CreateEntityAsync(user);
        return Created();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        User? user = await userRepository.GetUserByUsername(loginUserDto.Username);
        if (user is null)
        {
            return Unauthorized();
        }

        bool correctPassword = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.Password);
        if (!correctPassword)
        {
            return Unauthorized();
        }

        LoginResponseDto loginResponseDto = new LoginResponseDto
        {
            Token = JwtHandler.GenerateJwtToken(user, configuration)
        };
        return Ok(loginResponseDto);
    }
}