namespace Chirp.Core.DTO;

public class FollowDTO
{
    public int Id { get; set; }
    public required string FollowedId { get; set; }
    public required string FollowerId { get; set; }
}