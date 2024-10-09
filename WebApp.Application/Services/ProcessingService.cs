using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.Interfaces.Repositories;
using WebApp.Core.Interfaces.Services;
using WebApp.Domain;

namespace WebApp.Application.Services;

public class ProcessingService : IProcessingService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMemoryCache _cache;
    private readonly int _processingDelay;

    public ProcessingService(IServiceScopeFactory scopeFactory, IMemoryCache cache, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _cache = cache;
        _processingDelay = int.Parse(configuration["ProcessingDelay"]) * 1000;
    }

    public async Task ProcessRequestAsync(UserStatisticRequest request)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            int percent = request.Percent;
            var repository = scope.ServiceProvider.GetRequiredService<IUserStatisticsRepository>();

            const int steps = 100;
            const int saveStep = 10;

            for (int i = percent; i <= steps; i++)
            {
                await Task.Delay(_processingDelay / steps);

                // Обновляем кэш на каждом шаге
                _cache.Set(request.Id.ToString(), i);

                // Сохраняем прогресс каждые saveStep шагов или на последнем шаге
                if (i % saveStep == 0 || i == steps)
                {
                    request.Percent = i;
                    await repository.UpdateAsync(request);
                }
            }

            // Создание результата
            var result = new Result
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CountSignIn = new Random().Next(1, 20),
                UserStatisticId = request.Id
            };

            await repository.CreateResultAsync(result);
        }
    }
}
