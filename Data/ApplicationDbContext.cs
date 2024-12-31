using Microsoft.EntityFrameworkCore;
using NotificationSystem.Models.Domain;

namespace NotificationSystem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
{
    public DbSet<Notification> Notifications { get; set; }
}