using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationSystem.Services.Interfaces;

namespace NotificationSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/notifications")]
public class NotificationController(INotificationService notificationService): ControllerBase
{
    [HttpGet("user/page/{page:int}/page-size/{pageSize:int}")]
    public async Task<IActionResult> GetNotifications([FromRoute] int page, [FromRoute] int pageSize)
    {
        var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await notificationService.GetNotificationsAsync(id, page, pageSize);
        return Ok(result);
    }
}          