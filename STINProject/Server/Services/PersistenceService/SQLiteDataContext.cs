using Microsoft.EntityFrameworkCore;
using STINProject.Server.Services.PersistenceService.Models;

namespace STINProject.Server.Services.PersistenceService
{
    public class SQLiteDataContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; private set; }
        public DbSet<Account> Accounts { get; private set; }
        public DbSet<User> Users { get; private set; }

        private readonly string _connectionString = string.Empty;
        public SQLiteDataContext()
        {
            // TODO add hidden prod connection string
            _connectionString = "Data Source=c:\\mydb.db;Version=3;";
        }

        public SQLiteDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SQLiteDataContext(DbContextOptions<SQLiteDataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*
            modelBuilder.Entity<Account>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.OwnerId);
            */
        }

    }
}
