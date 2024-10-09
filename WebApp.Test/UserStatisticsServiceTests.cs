using Microsoft.Extensions.Caching.Memory;
using Moq;
using WebApp.Application.Services;
using WebApp.Core.Interfaces.Repositories;
using WebApp.Core.Interfaces.Services;
using WebApp.Domain;

public class UserStatisticsServiceTests
{
    private readonly Mock<IUserStatisticsRepository> _repositoryMock;
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly Mock<IProcessingService> _processingServiceMock;
    private readonly UserStatisticsService _service;

    public UserStatisticsServiceTests()
    {
        _repositoryMock = new Mock<IUserStatisticsRepository>();
        _cacheMock = new Mock<IMemoryCache>();
        _processingServiceMock = new Mock<IProcessingService>();
        _service = new UserStatisticsService(_repositoryMock.Object, _cacheMock.Object, _processingServiceMock.Object);
    }

    [Fact]
    public async Task CreateUserStatisticsAsync_ShouldThrowException_WhenStartDateIsLaterThanEndDate()
    {
        var userId = "user123";
        var startDate = DateTime.UtcNow.AddDays(1);
        var endDate = DateTime.UtcNow;

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateUserStatisticsAsync(userId, startDate, endDate));
        Assert.Equal("The start date cannot be later than the end date. (Parameter 'startDate')", exception.Message);
    }

    [Fact]
    public async Task GetStatisticsInfoAsync_ShouldReturnNull_WhenUserStatisticRequestNotFound()
    {
        var queryId = Guid.NewGuid();
        _repositoryMock.Setup(x => x.GetByIdAsync(queryId)).ReturnsAsync((UserStatisticRequest)null);

        var result = await _service.GetStatisticsInfoAsync(queryId);

        Assert.Null(result);
    }
}

