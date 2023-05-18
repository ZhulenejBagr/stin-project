using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STINProject.Server.Services.PersistenceService.Models;
using System.Globalization;

namespace STINProject.Server.Services.PersistenceService
{
    public class SQLiteDataContext : DbContext
    {
        public IConfiguration _config;
        public DbSet<Transaction> Transactions { get; private set; }
        public DbSet<Account> Accounts { get; private set; }
        public DbSet<User> Users { get; private set; }

        //private readonly string _connectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banking.db");
        public SQLiteDataContext(IConfiguration config)
        {
            _config = config;
            // TODO add hidden prod connection string
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //_connectionString = Path.Combine(path, "banking.db");
        }

        public SQLiteDataContext(IConfiguration config, DbContextOptions<SQLiteDataContext> options) : base(options) 
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var cstring = _config.GetConnectionString("SQLite");
                optionsBuilder.UseSqlite(cstring);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var sampleUser = new User { Username = "sampleuser", Password = "password", Email = "sampleuser@gmail.com", UserId = Guid.NewGuid() };
            var sampleAccount = new Account { AccountId = Guid.NewGuid(), Balance = 100, Currency = "CZK", OwnerId = sampleUser.UserId };
            var sampleAccount2 = new Account { AccountId = Guid.NewGuid(), Balance = 0, Currency = "USD", OwnerId = sampleUser.UserId };
            var sampleTransaction1 = new Transaction { AccountID = sampleAccount.AccountId, TransactionID = Guid.NewGuid(), Value = 20, Date = DateTime.ParseExact("24.12.2022", "dd.mm.yyyy", CultureInfo.InvariantCulture) };
            var sampleTransaction2 = new Transaction { AccountID = sampleAccount.AccountId, TransactionID = Guid.NewGuid(), Value = -30, Date = DateTime.ParseExact("05.05.2022", "dd.mm.yyyy", CultureInfo.InvariantCulture) };
            var sampleTransaction3 = new Transaction { AccountID = sampleAccount2.AccountId, TransactionID = Guid.NewGuid(), Value = -100, Date = DateTime.ParseExact("10.08.2022", "dd.mm.yyyy", CultureInfo.InvariantCulture) };
            

            modelBuilder.Entity<User>()
                .HasData(sampleUser);

            modelBuilder.Entity<Account>()
                .HasData(sampleAccount);
            modelBuilder.Entity<Account>()
                .HasData(sampleAccount2);

            modelBuilder.Entity<Transaction>()
                .HasData(new Transaction[] { sampleTransaction1, sampleTransaction2, sampleTransaction3 });


            /*
            modelBuilder.Entity<Account>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.OwnerId);
            */
        }

    }
}
