using Finlytics.Application.DTOs;
using Finlytics.Application.Interfaces;
using Finlytics.Application.Interfaces.Repositories;
using Finlytics.Domain.Entities;
using Bcrypt = BCrypt.Net.BCrypt;

namespace Finlytics.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly JwtService _jwtService;
    private readonly IMongoRepository<User> _repository;

    public UserService(IMongoRepository<User> repository , JwtService jwtService)
    {
        _repository = repository;
        _jwtService = jwtService;
    }

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

    public async Task<string> LoginAsync(LoginUserDto dto)
    {
        var user = (await _repository.FilterByAsync(u => u.Email == dto.Email)).FirstOrDefault();
        if (user == null || !Bcrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return _jwtService.GenerateToken(user.Id, user.Email);
    }


    public async Task<UserProfileDto> GetProfileAsync(string userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        return user == null
            ? throw new Exception("User not found")
            : new UserProfileDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }
}
