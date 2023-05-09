using Microsoft.EntityFrameworkCore;
using STINProject_API.Services.PersistenceService.Model;
using STINProject_API.Services.PersistenceService.Models;

namespace STINProject_API.Services.PersistenceService
{
    public class SQLiteDataContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; private set; }
        public DbSet<Account> Accounts { get; private set; }
        public DbSet<User> Users { get; private set; }

        private readonly string _connectionString = string.Empty;
        public SQLiteDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SQLiteDataContext(DbContextOptions<SQLiteDataContext> options) : base(options)
        {
        }
        
        public SQLiteDataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

    }
}
