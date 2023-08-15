namespace API.Models;


public class MessageDTO
{
    public MessageDTO()
    {
        SentDate = DateTime.UtcNow;
        Tags = new List<string>();
    }

    public string Message { get; set; } = null!;
    public DateTime SentDate { get; set; }
    public IEnumerable<string> Tags { get; set; }
}
