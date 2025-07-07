using Finlytics.Application.DTOs;

namespace Finlytics.Application.Interfaces;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterUserDto dto);
    Task<string> LoginAsync(LoginUserDto dto);
    Task<UserProfileDto> GetProfileAsync(string userId);
}
