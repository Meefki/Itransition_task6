namespace Application.Network;

public interface IEventService
{
    Task Send(MessageNotification message);
}
