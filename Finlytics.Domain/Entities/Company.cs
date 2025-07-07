using Finlytics.Domain.Interfaces;

namespace Finlytics.Domain.Entities;

public class Company : IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string OwnerUserId { get; set; } = string.Empty;
}
