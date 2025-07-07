using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        var userId = await _userService.LoginAsync(dto);
        return Ok(new { UserId = userId });
    }

    [Authorize]
    [HttpGet("profile/{id}")]
    public async Task<IActionResult> GetProfile(string id)
    {
        var profile = await _userService.GetProfileAsync(id);
        return Ok(profile);
    }

}
