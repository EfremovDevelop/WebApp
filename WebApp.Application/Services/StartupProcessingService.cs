using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Core.Interfaces.Services;

namespace WebApp.Application.Services;

public class StartupProcessingService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public StartupProcessingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var userStatisticsService = scope.ServiceProvider.GetRequiredService<IUserStatisticsService>();

            // Получаем незавершенные запросы и запускаем их обработку
            await userStatisticsService.ProcessUnfinishedRequestsAsync();
        }
    }
}

