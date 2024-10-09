using WebApp.Domain;

namespace WebApp.Core.Interfaces.Services
{
    public interface IProcessingService
    {
        Task ProcessRequestAsync(UserStatisticRequest request);
    }
}
