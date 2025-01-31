using NotificationSystem.Models.Domain;
using NotificationSystem.Services.Interfaces;

namespace NotificationSystem.Services.Implementations;

public class ConsoleNotifier: INotifier
{
    public async Task Notify(Notification notification, string deviceToken)
    {
        Console.WriteLine($"Notification: {notification.PostId}, UserDevice = {deviceToken}, Content = {notification.Content}");
    }
}