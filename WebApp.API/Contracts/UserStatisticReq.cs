namespace WebApp.API.Contracts;

public record UserStatisticReq (string UserId, DateTime StartDate, DateTime EndDate);
