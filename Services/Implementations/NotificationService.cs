using NotificationSystem.Models.Domain;
using NotificationSystem.Models.Dto;
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
        IEnumerable<UserToNotifyDto> subscribes;
        do
        {
            subscribes = await userService.GetSubscribers(userId, page, pageSize);
            page++;
            var notifications = new List<Notification>();
            foreach (var subscriber in subscribes)
            {
                var notification = new Notification
                    { PostId = postId, UserId = subscriber.Id, Content = $"Новый пост от пользователя с id {userId}" };
                notifications.Add(notification);
                await notifier.Notify(notification, subscriber.DeviceToken);
            }
            await notificationRepository.BulkInsertAsync(notifications);
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