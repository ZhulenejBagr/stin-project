﻿using STINProject.Server.Services.PersistenceService.Models;
using System.Diagnostics.CodeAnalysis;

namespace STINProject.Server.Services.PersistenceService
{
    public interface IPersistenceService
    {
        public User GetUser(Guid id);
        public User GetUser(string username);
        public bool AddUser(User user);
        public IEnumerable<Account> GetAccounts(Guid userId);
        public bool AddAccount(Account account);
        public IEnumerable<Transaction> GetTransactions(Guid accountId);
        public bool AddTransaction(Transaction transaction);

    }
}
