using Microsoft.Extensions.Options;
using NotificationSystem.Models.Domain;
using NotificationSystem.Services.Interfaces;

namespace NotificationSystem.Services.Implementations;

public class FcmNotifier(IOptions<Fcm> fcmOptions, ILogger<FcmNotifier> logger): INotifier
{
    public async Task Notify(Notification notification, string deviceToken)
    {
        throw new NotImplementedException();
    }
}