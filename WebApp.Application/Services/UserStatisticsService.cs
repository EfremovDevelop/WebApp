using Microsoft.Extensions.Caching.Memory;
using WebApp.Core.DtoModels;
using WebApp.Core.Interfaces.Repositories;
using WebApp.Core.Interfaces.Services;
using WebApp.Domain;

namespace WebApp.Application.Services;

public class UserStatisticsService : IUserStatisticsService
{
    private readonly IUserStatisticsRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly IProcessingService _processingService;

    public UserStatisticsService(
        IUserStatisticsRepository repository,
        IMemoryCache cache,
        IProcessingService processingService)
    {
        _repository = repository;
        _cache = cache;
        _processingService = processingService;
    }

    public async Task<Guid> CreateUserStatisticsAsync(string userId, DateTime startDate, DateTime endDate)
    {
        if (startDate >  endDate)
        {
            throw new ArgumentException("The start date cannot be later than the end date.", nameof(startDate));
        }
        var request = new UserStatisticRequest
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(request);

        // Запускаем фоновую обработку запроса
        _ = _processingService.ProcessRequestAsync(request);

        return request.Id;
    }

    public async Task<UserStatisticRequestDto?> GetStatisticsInfoAsync(Guid queryId)
    {
        var userStatisticRequest = await _repository.GetByIdAsync(queryId);
        if (userStatisticRequest == null) return null;

        // Получаем прогресс из кэша или запроса
        int percent = _cache.TryGetValue(queryId.ToString(), out int cachedPercent)
            ? cachedPercent
            : userStatisticRequest.Percent;

        var result = await _repository.GetResultByUserStatisticIdAsync(queryId);

        return new UserStatisticRequestDto
        {
            Id = userStatisticRequest.Id,
            Percent = percent,
            Result = result != null ? new ResultDto
            {
                UserId = result.UserId,
                CountSignIn = result.CountSignIn
            } : null
        };
    }

    public async Task ProcessUnfinishedRequestsAsync()
    {
        var unfinishedRequests = await _repository.GetUnfinishedRequestsAsync();
        foreach (var request in unfinishedRequests)
        {
            _ = _processingService.ProcessRequestAsync(request);
        }
    }
}
