using Microsoft.EntityFrameworkCore;
using STINProject_API.Services.PersistenceService.Model;
using STINProject_API.Services.PersistenceService.Models;

namespace STINProject_API.Services.PersistenceService
{
    public class SQLitePersistenceService : DbContext, IPersistenceService
    {
        private DbSet<Transaction> _transactions;
        private DbSet<Account> _accounts;
        private DbSet<User> _users;

        private string DbPath;

        public SQLitePersistenceService()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "bank.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source={DbPath}");
        }

        public bool AddTransaction(Transaction transaction)
        {
            this.Add(transaction);
            return this.SaveChanges() == 1;
        }

        public IEnumerable<Account> GetAccounts(Guid userId)
        {
            return this._accounts.Where(x => x.OwnerId == userId);
        }

        public IEnumerable<Transaction> GetTransactions(Guid accountId)
        {
            return this._transactions.Where(x => x.AccountID == accountId);
        }

        public User GetUser(Guid id)
        {
            return this._users.Single(x => x.UserId == id);
        }

        public User GetUser(string username)
        {
            return this._users.Single(x => x.Username == username);
        }
    }
}
