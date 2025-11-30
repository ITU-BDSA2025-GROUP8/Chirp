namespace Chirp.Infrastructure.Entities;

public class Follow
{
    public int FollowId { get; set; }
    public string FollowedId { get; set; } = String.Empty;
    public string FollowerId { get; set; } = String.Empty;
}