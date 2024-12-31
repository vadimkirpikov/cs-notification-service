using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NotificationSystem.Data;
using NotificationSystem.Models.Domain;
using NotificationSystem.Repositories.Interfaces;

namespace NotificationSystem.Repositories.Implementations;

public class NotificationRepository(ApplicationDbContext context): INotificationRepository
{
    public async Task CreateAsync(Notification notification)
    {
        await context.Notifications.AddAsync(notification);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Notification>> GetNotificationsAsync(int userId, int page, int pageSize)
    {
        return await context.Notifications.Where(n => n.UserId.Equals(userId))
            .OrderBy(n => n.Id)
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task BulkInsertAsync(IEnumerable<Notification> notifications)
    {
        await context.BulkInsertAsync(notifications);
    }
}