using Finlytics.Domain.Entities;
using Bcrypt = BCrypt.Net.BCrypt;
using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Finlytics.Application.Interfaces.Repositories;

namespace Finlytics.Infrastructure.Services;

// Handles user registration, login, and profile operations
public class UserService(IMongoRepository<User> repository, JwtService jwtService) : IUserService
{
    private readonly JwtService _jwtService = jwtService;
    private readonly IMongoRepository<User> _repository = repository;

    #region Login

    // Authenticates user and returns JWT token
    public async Task<string> LoginAsync(LoginUserDto dto)
    {
        var user = (await _repository.FilterByAsync(u => u.Email == dto.Email)).FirstOrDefault();
        if (user == null || !Bcrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return _jwtService.GenerateToken(user.Id, user.Email);
    }

    #endregion

    #region Register

    // Registers a new user after validating email uniqueness
    public async Task<string> RegisterAsync(RegisterUserDto dto)
    {
        var existing = await _repository.FilterByAsync(u => u.Email == dto.Email);
        if (existing.Any())
            throw new Exception("Email already exists.");

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = Bcrypt.HashPassword(dto.Password)
        };

        await _repository.AddAsync(user);
        return user.Id;
    }

    #endregion

    #region Get Profile

    // Retrieves profile information by user ID
    public async Task<UserProfileDto> GetProfileAsync(string userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        return new UserProfileDto
        {
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }

    #endregion

    #region Update Profile

    // Updates user's profile data
    public async Task UpdateProfileAsync(string userId, UpdateProfileDto dto)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        user.Username = dto.Username;
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;

        await _repository.UpdateAsync(user);
    }

    #endregion
}
