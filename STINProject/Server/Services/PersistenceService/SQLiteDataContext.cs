using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STINProject.Server.Services.PersistenceService.Models;

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

            modelBuilder.Entity<User>()
                .HasData(new User { Username = "sus", Password = "bus", Email = "sus@bus.com", UserId = Guid.NewGuid() });
            /*
            modelBuilder.Entity<Account>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.OwnerId);
            */
        }

    }
}
