

using Confluent.Kafka;
using NotificationSystem.HostedService;
using NotificationSystem.Repositories.Implementations;
using NotificationSystem.Repositories.Interfaces;
using NotificationSystem.Services.Implementations;
using NotificationSystem.Services.Interfaces;

namespace NotificationSystem.Utils.Extensions;

public static class CustomDiManager
{
    public static WebApplicationBuilder InjectDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient("UserClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["UserServiceUrl"]!);
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                UseProxy = false
            })
            .Services
            .AddHostedService<NotificationConsumer>()
            .AddTransient<HttpClient>(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("UserClient"))
            .AddTransient<IConsumer<Null, string>>(sp =>
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = builder.Configuration["Kafka:BootstrapServers"],
                    GroupId = builder.Configuration["Kafka:GroupId"],
                };
                var consumer = new ConsumerBuilder<Null, string>(config).Build();
                return consumer;
            })
            .AddTransient<INotifier, ConsoleNotifier>()
            .AddTransient<INotificationRepository, NotificationRepository>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<INotificationService, NotificationService>();
        return builder;
    }
}