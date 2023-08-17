using Application.Network;
using Domain;

namespace API.Network;

public interface IClient
{
    Task NewMessage(MessageNotification message);
    Task NewTags();
}
