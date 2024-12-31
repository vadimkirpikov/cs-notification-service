using NotificationSystem.Models.Domain;

namespace NotificationSystem.Services.Interfaces;

public interface INotifier
{
    void Notify(Notification notification);
}