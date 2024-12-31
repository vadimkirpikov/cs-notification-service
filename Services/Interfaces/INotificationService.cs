using NotificationSystem.Models.Domain;

namespace NotificationSystem.Services.Interfaces;

public interface INotificationService
{
    Task NotifyAll(int userId, int postId);
    Task<IEnumerable<Notification>> GetNotificationsAsync(int userId, int page, int pageSize);
}