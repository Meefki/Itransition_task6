using Application.Network;
using Application.UseCases;
using Domain;
using Microsoft.AspNetCore.SignalR;

namespace API.Network;

public class MessageHub : Hub<IClient>, IEventService
{
    private readonly IMessageService messageService;

    public MessageHub(IMessageService messageService)
    {
        this.messageService = messageService;
    }

    public async Task Send(Message message)
    {
        IEnumerable<Tag> newTags = await messageService.WriteMessageAsync(message);

        await Clients.Others.NewTags(newTags);
        await Clients.Others.NewMessage(message);
    }
}
