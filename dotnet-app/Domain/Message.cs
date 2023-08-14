namespace Domain;

public record Message
{
    public Message() { }
    public Message(string value, ISet<Tag> tags)
    {
        _tags = tags;
        Value = value;
        SentDate = DateTime.UtcNow;
    }

    public string Value { get; init; } = null!;
    public DateTime SentDate { get; init; }

    private readonly ISet<Tag> _tags = null!;
    public IReadOnlySet<Tag> Tags => (IReadOnlySet<Tag>)_tags;
}
