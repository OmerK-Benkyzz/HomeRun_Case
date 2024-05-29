using Core.Domain.Entities;

namespace Notification.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid ProviderId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
}