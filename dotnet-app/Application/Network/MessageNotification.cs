using Domain;

namespace Application.Network;

public class MessageNotification
{
    public MessageNotification()
    {
        Tags = new List<string>();
    }

    public string Value { get; init; } = null!;
    public IReadOnlyCollection<string> Tags { get; init; } = null!;
}
