using Microsoft.EntityFrameworkCore;
using WebApp.Core.Interfaces.Repositories;
using WebApp.Domain;

namespace WebApp.DataAccess.Repositories;

public class UserStatisticsRepository : IUserStatisticsRepository
{
    private readonly AppDbContext _context;

    public UserStatisticsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(UserStatisticRequest userStatistic)
    {
        await _context.UserStatisticRequests.AddAsync(userStatistic);
        await SaveChangesAsync();
        return userStatistic.Id;
    }

    public async Task CreateResultAsync(Result result)
    {
        await _context.Results.AddAsync(result);
        await SaveChangesAsync();
    }

    public async Task<UserStatisticRequest?> GetByIdAsync(Guid Id)
    {
        return await _context.UserStatisticRequests.FindAsync(Id);
    }

    public async Task<Result?> GetResultByUserStatisticIdAsync(Guid Id)
    {
        return await _context.Results.FirstOrDefaultAsync(x => x.UserStatisticId == Id);
    }

    public async Task UpdateAsync(UserStatisticRequest request)
    {
        await _context.UserStatisticRequests
       .Where(i => i.Id == request.Id)
       .ExecuteUpdateAsync(s => s
           .SetProperty(p => p.Percent, p => request.Percent));
        await SaveChangesAsync();
    }

    public async Task <List<UserStatisticRequest>> GetUnfinishedRequestsAsync()
    {
        return await _context.UserStatisticRequests
                    .Where(r => r.Percent < 100 && !_context.Results.Any(res => res.UserStatisticId == r.Id))
                    .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
