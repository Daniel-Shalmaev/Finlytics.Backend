using Finlytics.Domain.Interfaces;

namespace Finlytics.Domain.Entities;

public class User : IIdentifiable
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
