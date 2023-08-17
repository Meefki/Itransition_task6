namespace Infrastructure.Models;

public class MessageTagDTO
{
    public Guid MessageId { get; set; }
    public MessageDTO Message { get; set; } = null!;

    public Guid TagId { get; set; }
    public TagDTO Tag { get; set; } = null!;
}
