using System.Text.Json;

namespace NotificationSystem.Utils.Extensions;

public static class Deserializer
{
    public static T? Deserialize<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}