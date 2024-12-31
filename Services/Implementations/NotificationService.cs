using NotificationSystem.Models.Domain;
using NotificationSystem.Repositories.Interfaces;
using NotificationSystem.Services.Interfaces;

namespace NotificationSystem.Services.Implementations;

public class NotificationService(
    INotificationRepository notificationRepository,
    IUserService userService,
    INotifier notifier) : INotificationService
{
    public async Task NotifyAll(int userId, int postId)
    {
        var page = 1;
        const int pageSize = 500;
        IEnumerable<int> subscribes;
        do
        {
            subscribes = await userService.GetSubscribersIdentifiers(userId, page, pageSize);
            page++;
            var notifications = subscribes.Select(subId => new Notification
                { PostId = postId, UserId = userId, Content = $"Новый пост от пользователя с id {userId}" }).ToList();
            await notificationRepository.BulkInsertAsync(notifications);
            foreach (var notification in notifications) notifier.Notify(notification);
        } while (subscribes.Any());
    }

    public async Task<IEnumerable<Notification>> GetNotificationsAsync(int userId, int page, int pageSize)
    {
        if (page < 1 || pageSize > 1000 || pageSize < 1)
        {
            throw new ArgumentException("Invalid page or page size");
        }
        return await notificationRepository.GetNotificationsAsync(userId, page, pageSize);
    }
}