using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finlytics.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var userId = await _userService.RegisterAsync(dto);
        return Ok(new { UserId = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var token = await _userService.LoginAsync(dto);
        return Ok(new { token });
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var profile = await _userService.GetProfileAsync(userId);
        return Ok(profile);
    }
}
