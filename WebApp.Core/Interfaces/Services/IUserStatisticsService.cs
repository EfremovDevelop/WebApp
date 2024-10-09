using WebApp.Core.DtoModels;
using WebApp.Domain;

namespace WebApp.Core.Interfaces.Services;

public interface IUserStatisticsService
{
    Task<Guid> CreateUserStatisticsAsync(string userId, DateTime startDate, DateTime endDate);
    Task<UserStatisticRequestDto?> GetStatisticsInfoAsync(Guid queryId);
    Task ProcessUnfinishedRequestsAsync();
}