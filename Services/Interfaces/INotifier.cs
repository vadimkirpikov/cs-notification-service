using NotificationSystem.Models.Domain;

namespace NotificationSystem.Services.Interfaces;

public interface INotifier
{
    Task Notify(Notification notification, string deviceToken);
}