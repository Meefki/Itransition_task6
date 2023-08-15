namespace Infrastructure.Models;

public class MessageDTO
{
    public MessageDTO()
    {
        MessageTags = new();
    }

    public int Id { get; set; }
    public string Value { get; set; } = null!;
    public DateTime SentDate { get; set; } = DateTime.UtcNow;
    public List<MessageTagDTO> MessageTags { get; set; }
}
