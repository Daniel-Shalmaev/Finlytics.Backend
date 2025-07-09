using Finlytics.Application.DTOs;

namespace Finlytics.Application.Interfaces;

public interface IUserService
{
    Task<string> LoginAsync(LoginUserDto dto);
    Task<string> RegisterAsync(RegisterUserDto dto);
    Task<UserProfileDto> GetProfileAsync(string userId);
    Task<string> GetFirstNameByEmailAsync(string email);
    Task UpdateProfileAsync(string userId, UpdateProfileDto dto);
}
