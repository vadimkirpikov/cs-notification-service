using Confluent.Kafka;
using NotificationSystem.Models.Dto;
using NotificationSystem.Services.Interfaces;
using NotificationSystem.Utils.Extensions;

namespace NotificationSystem.HostedService;

public class NotificationConsumer(IConsumer<Null, string> consumer, IServiceScopeFactory serviceScopeFactory)
    : IHostedService
{
    private CancellationTokenSource? _cts;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {

        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        
        Task.Run(() => ConsumeMessages(_cts.Token), cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts!.CancelAsync();
        return Task.CompletedTask;
    }

    private async Task ConsumeMessages(CancellationToken cancellationToken)
    {
        try
        {
            consumer.Subscribe("post.published");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(cancellationToken);
                    var message = consumeResult.Message.Value;
                    var notification = message.Deserialize<NotificationDto>();
                    using var scope = serviceScopeFactory.CreateScope();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    await notificationService.NotifyAll(notification!.UserId, notification.PostId);
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"Ошибка потребления: {ex.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Ожидаем завершения
        }
        finally
        {
            consumer?.Close();
        }
    }
}