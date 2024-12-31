using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationSystem.Services.Interfaces;

namespace NotificationSystem.Controllers;

[Authorize]
[ApiController]
[Route("notifications")]
public class NotificationController(INotificationService notificationService): ControllerBase
{
    [HttpGet("user/{id:int}/page/{page:int}/page-size/{pageSize:int}")]
    public async Task<IActionResult> GetNotifications([FromRoute] int id, [FromRoute] int page, [FromRoute] int pageSize)
    {
        var result = await notificationService.GetNotificationsAsync(id, page, pageSize);
        return Ok(result);
    }
}