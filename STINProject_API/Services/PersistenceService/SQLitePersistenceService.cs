using STINProject_API.Services.PersistenceService.Model;
using STINProject_API.Services.PersistenceService.Models;

namespace STINProject_API.Services.PersistenceService
{
    public class SQLitePersistenceService : IPersistenceService
    {
        private readonly SQLiteDataContext _context;
        public SQLitePersistenceService(SQLiteDataContext context)
        {
            _context = context;
        }


        public IEnumerable<Account> GetAccounts(Guid userId)
        {
            return _context.Accounts.Where(x => x.OwnerId == userId);
        }

        public IEnumerable<Transaction> GetTransactions(Guid accountId)
        {
            return _context.Transactions.Where(x => x.AccountID == accountId);
        }

        public User GetUser(Guid id)
        {
            return _context.Users.First(x => x.UserId == id);
        }

        public User GetUser(string username)
        {

            return _context.Users.First(x => x.Username == username);
        }
        public bool AddTransaction(Transaction transaction)
        {
            return _context.Users.Single(x => x.Username == username);
        }
        public bool AddTransaction(Transaction transaction)
        {
           return TryAddObject(transaction);
        }

        public bool AddAccount(Account account)
        {
            return TryAddObject(account);
        }

        public bool AddUser(User user)
        {
            return TryAddObject(user);
        }
 
        private bool TryAddObject(object obj) 
        {
            var status = false;
            try
            {
                _context.Add(obj);
                status = _context.SaveChanges() > 0;
            }
            catch
            {
                status = false;
            }

            return status;
        }
    }
}
