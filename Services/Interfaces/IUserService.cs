namespace NotificationSystem.Services.Interfaces;

public interface IUserService
{
    Task<List<int>> GetSubscribersIdentifiers(int userId, int page, int pageSize);
}