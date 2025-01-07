using NotificationSystem.Models.Dto;

namespace NotificationSystem.Services.Interfaces;

public interface IUserService
{
    Task<List<UserToNotifyDto>> GetSubscribers(int userId, int page, int pageSize);
}