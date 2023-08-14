namespace API.Models;


public class MessageDTO
{
    public string Message { get; set; } = null!;
    public DateTime SentDate { get; set; } = DateTime.UtcNow;
    public IEnumerable<string> Tags { get; set; } = null!;
}
