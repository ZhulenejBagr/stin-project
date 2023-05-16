using Microsoft.EntityFrameworkCore;
using STINProject.Server.Services.PersistenceService.Models;

namespace STINProject.Server.Services.PersistenceService
{
    public class SQLiteDataContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; private set; }
        public DbSet<Account> Accounts { get; private set; }
        public DbSet<User> Users { get; private set; }

        //private readonly string _connectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banking.db");
        public SQLiteDataContext()
        {
            // TODO add hidden prod connection string
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //_connectionString = Path.Combine(path, "banking.db");
        }

        public SQLiteDataContext(DbContextOptions<SQLiteDataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=Database/banking.db");
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
