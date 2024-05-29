namespace Notification.Domain.Dtos;

public class NotificationDto
{
    public Guid ProviderId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
}