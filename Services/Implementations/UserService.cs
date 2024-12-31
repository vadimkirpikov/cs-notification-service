using NotificationSystem.Models.Dto;
using NotificationSystem.Repositories.Interfaces;
using NotificationSystem.Services.Interfaces;

namespace NotificationSystem.Services.Implementations;

public class UserService(HttpClient userClient): IUserService
{
    public async Task<List<int>> GetSubscribersIdentifiers(int userId, int page, int pageSize)
    {
        const string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkFkbWluVXNlciIsInJvbGUiOiJBZG1pbmlzdHJhdG9yIiwibmJmIjoxNzM1NjUxNDg0LCJleHAiOjE3MzU2NTUwODQsImlhdCI6MTczNTY1MTQ4NH0.ZGQ7FpHH6TYADZc1Um6Bi3BhDyHOyEYbranmQ0xKa6A"; // Замените на реальный токен

        var requestMessage = new HttpRequestMessage(HttpMethod.Get, 
            $"api/v1/users/{userId}/subscribers/page/{page}/page-size/{pageSize}");
    
        requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await userClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();
        var users = await response.Content.ReadFromJsonAsync<List<UserToNotifyDto>>();
        return users != null ? users.Select(u => u.Id).ToList() : [];
    }
}