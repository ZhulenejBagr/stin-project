﻿using STINProject_API.Services.PersistenceService.Model;
using STINProject_API.Services.PersistenceService.Models;

namespace STINProject_API.Services.PersistenceService
{
    public interface IPersistenceService
    {
        public User GetUser(Guid id);
        public User GetUser(string username);
        public IEnumerable<Account> GetAccounts(Guid userId);
        public IEnumerable<Transaction> GetTransactions(Guid accountId);
        public bool AddTransaction(Transaction transaction);

    }
}