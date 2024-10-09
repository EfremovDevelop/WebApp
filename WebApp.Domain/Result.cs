namespace WebApp.Domain;

public class Result
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public int CountSignIn { get; set;}

    public Guid UserStatisticId { get; set;}
    public virtual UserStatisticRequest UserStatistic { get; set; }
}
