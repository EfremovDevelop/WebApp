namespace WebApp.Domain;

public class UserStatisticRequest
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Percent { get; set; } = 0;

    public virtual Result Result { get; set; }
}
