using Application.Repositories;
using Domain;
using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Infrastructure.Repository;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext context;

    public TagRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Tag>> CheckExistingAsync(IEnumerable<Tag> tags)
    {
        List<string> names = tags.Select(t => t.Name).ToList();
        return await Task.Run(() => context.Tags.Where(x => names.Contains(x.Name)).ToList());
    }

    public async Task<ITransaction> CreateAsync(IEnumerable<Tag> tags, ITransaction? transaction = null)
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

        await context.Tags.AddRangeAsync(tags);
        await context.SaveChangesAsync();

        return transaction;
    }

    public async Task<IEnumerable<Tag>> GetAsync(string startWith)
    {
        return await Task.Run(() => context.Tags.Where(x => x.Name.StartsWith(startWith)).ToList());
    }
}
