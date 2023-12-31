﻿using Application.Repositories;
using Domain;
using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;
using Infrastructure.Models;

namespace Infrastructure.Repository;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext context;

    public MessageRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<ITransaction> CreateAsync(Message message, ITransaction? transaction = null)
    {
        if (transaction is null)
        {
            var dbTransaction = (await context.Database.BeginTransactionAsync()).GetDbTransaction();
            transaction = new Transaction(dbTransaction);
        }
        else
        {
            await context.Database.UseTransactionAsync((DbTransaction)transaction.Value);
        }

        await context.Messages.AddAsync(Mapper.MapEntityToDto(message));
        await context.SaveChangesAsync();

        return transaction;
    }

    public async Task<IEnumerable<Message>> GetAsync(IEnumerable<string> tags)
    {
        Expression<Func<MessageDTO, bool>> predicate = null!;
        if (tags.Any())
            predicate = x => x.MessageTags.Any(t => tags.Contains(t.Tag.Name)) || x.MessageTags.Count == 0;
        else
            predicate = x => true;

        return await Task.Run(
            () => context.Messages
                .Include(x => x.MessageTags)
                .ThenInclude(x => x.Tag)
                .Where(predicate)
                .Select(x => Mapper.MapDtoToEntity(x))
                .ToList());
    }

    public async Task<ITransaction> LinkTagsAsync(Message message, IEnumerable<Tag> tags, ITransaction? transaction = null)
    {
        if (transaction is null)
        {
            var dbTransaction = (await context.Database.BeginTransactionAsync()).GetDbTransaction();
            transaction = new Transaction(dbTransaction);
        }
        else
        {
            await context.Database.UseTransactionAsync((DbTransaction)transaction.Value);
        }

        MessageDTO msgDTO = Mapper.MapEntityToDto(message);
        IEnumerable<TagDTO> tagDTOs = tags.Select(x => Mapper.MapEntityToDto(x));

        IEnumerable<MessageTagDTO> messageTagDTO = tagDTOs.Select(
            x => new MessageTagDTO()
            {
                MessageId = msgDTO.Id,
                TagId = x.Id
            }).ToList();

        await context.MessageTags.AddRangeAsync(messageTagDTO);
        await context.SaveChangesAsync();

        return transaction;
    }
}
