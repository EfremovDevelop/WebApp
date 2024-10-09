using Microsoft.AspNetCore.Mvc;
using WebApp.API.Contracts;
using WebApp.Core.Interfaces.Services;

namespace WebApp.API.Controllers
{
    [Route("report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IUserStatisticsService _userStatisticsService;

        public ReportController(IUserStatisticsService userStatisticsService)
        {
            _userStatisticsService = userStatisticsService;
        }

        [HttpPost("user_statistics")]
        public async Task<IActionResult> CreateUserStatistics([FromBody] UserStatisticReq request)
        {
            var resultId = await _userStatisticsService.CreateUserStatisticsAsync(request.UserId, request.StartDate, request.EndDate);
            return Ok(resultId);
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetStatisticsInfo(Guid queryId)
        {
            var response = await _userStatisticsService.GetStatisticsInfoAsync(queryId);
            if (response == null) return NotFound();

            return Ok(response);
        }
    }
}
