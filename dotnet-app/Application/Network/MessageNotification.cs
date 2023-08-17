using Domain;

namespace Application.Network;

public class MessageNotification
{
    public MessageNotification()
    {
        Tags = new List<string>();
    }

    public string Message { get; init; } = null!;
    public IReadOnlyCollection<string> Tags { get; init; } = null!;
    public DateTime? SentDate { get; set; } = DateTime.UtcNow;
}
