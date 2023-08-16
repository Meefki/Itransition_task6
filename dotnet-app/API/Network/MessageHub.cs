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
        IEnumerable<Tag> newTags = await messageService.WriteAsync(message);

        await Clients.All.NewTags(newTags);
        await Clients.All.NewMessage(message);
    }
}
