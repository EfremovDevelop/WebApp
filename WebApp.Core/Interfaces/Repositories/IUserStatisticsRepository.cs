using WebApp.Domain;

namespace WebApp.Core.Interfaces.Repositories;

public interface IUserStatisticsRepository
{
    Task<Guid> CreateAsync(UserStatisticRequest userStatistic);
    Task CreateResultAsync(Result result);
    Task<UserStatisticRequest?> GetByIdAsync(Guid Id);
    Task<Result?> GetResultByUserStatisticIdAsync(Guid Id);
    Task SaveChangesAsync();
    Task UpdateAsync(UserStatisticRequest request);
    Task<List<UserStatisticRequest>> GetUnfinishedRequestsAsync();
}