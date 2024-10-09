using WebApp.Domain;

namespace WebApp.Core.DtoModels;

public class UserStatisticRequestDto
{
    public Guid Id { get; set; }
    public int Percent { get; set; }
    public ResultDto? Result { get; set; }
}

public class ResultDto
{
    public string UserId { get; set; }
    public int CountSignIn { get; set; }
}
