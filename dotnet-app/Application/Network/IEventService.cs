using Domain;

namespace Application.Network;

public interface IEventService
{
    Task Send(Message message);
}
