using Domain;

namespace API.Network;

public interface IClient
{
    Task NewMessage(Message message);
    Task NewTags(IEnumerable<Tag> tags);
}
