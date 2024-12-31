using NotificationSystem.Models.Domain;
using NotificationSystem.Services.Interfaces;

namespace NotificationSystem.Services.Implementations;

public class ConsoleNotifier: INotifier
{
    public void Notify(Notification notification)
    {
        Console.WriteLine($"Notification: {notification.PostId}, User = {notification.Id}, Content = {notification.Content}");
    }
}