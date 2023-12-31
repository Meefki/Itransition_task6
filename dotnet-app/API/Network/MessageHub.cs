﻿using Application.Network;
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

    public async Task Send(MessageNotification message)
    {
        HashSet<Tag> tags = message.Tags.Select(x => new Tag(x)).ToHashSet();
        Message msg = new(message.Message, tags);

        IEnumerable<Tag> newTags = await messageService.WriteAsync(msg);

        await Clients.All.NewTags();
        await Clients.All.NewMessage(new MessageNotification() 
        { 
            Message = message.Message, 
            Tags = message.Tags, 
            SentDate = msg.SentDate 
        });
    }
}
