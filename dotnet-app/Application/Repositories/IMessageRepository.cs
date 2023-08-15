﻿using Domain;

namespace Application.Repositories;

public interface IMessageRepository
{
    Task<ITransaction> CreateAsync(Message message, ITransaction? transaction = null);
    Task<IEnumerable<Message>> GetAsync(IEnumerable<string> tags);
}
