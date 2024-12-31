using NotificationSystem.Models.Domain;

namespace NotificationSystem.Repositories.Interfaces;

public interface INotificationRepository
{
    Task CreateAsync(Notification notification);
    Task<IEnumerable<Notification>> GetNotificationsAsync(int userId, int page, int pageSize);
    Task BulkInsertAsync(IEnumerable<Notification> notifications);
}