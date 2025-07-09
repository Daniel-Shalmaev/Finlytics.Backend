using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finlytics.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    #region Register

    // Registers a new user and returns a JWT token.
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var userId = await _userService.RegisterAsync(dto);

        var token = await _userService.LoginAsync(new LoginUserDto
        { Email = dto.Email, Password = dto.Password });

        var firstName = await _userService.GetFirstNameByEmailAsync(dto.Email);

        return Ok(new { token, firstName });
    }

    #endregion

    #region Login

    // Authenticates a user and returns a JWT token.
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var token = await _userService.LoginAsync(dto);
        var firstName = await _userService.GetFirstNameByEmailAsync(dto.Email);

        return Ok(new { token, firstName });
    }

    #endregion

    #region Get Profile

    // Returns the profile of the currently authenticated user.
    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var profile = await _userService.GetProfileAsync(userId);
        return Ok(profile);
    }

    #endregion

    #region Update Profile

    // Updates profile details of the authenticated user.
    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userService.UpdateProfileAsync(userId, dto);
        return NoContent();
    }

    #endregion
}