using Application.Repositories;
using Domain;
using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

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

        await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();

        return transaction;
    }

    public async Task<IEnumerable<Message>> GetAsync(int pageSize, int page, IEnumerable<Tag> tags)
    {
        return await Task.Run(() => context.Messages.Skip(pageSize * page).Take(pageSize).ToList());
    }
}
