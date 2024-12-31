using System.ComponentModel.DataAnnotations;

namespace NotificationSystem.Models.Domain;

public class Notification
{
    public int Id { get; set; } 
    public required int PostId { get; set; }
    public required int UserId { get; set; }
    [MaxLength(100)]
    public required string Content { get; set; }
}