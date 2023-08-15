namespace Infrastructure.Models;

public class MessageTagDTO
{
    public int MessageId { get; set; }
    public MessageDTO Message { get; set; } = null!;

    public int TagId { get; set; }
    public TagDTO Tag { get; set; } = null!;
}
