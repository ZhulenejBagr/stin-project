using Bogus;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using STINProject_API.Services.PersistenceService;
using STINProject_API.Services.PersistenceService.Model;
using STINProject_API.Services.PersistenceService.Models;
using Xunit;

namespace STINProject_API.Tests.Services
{
    public class SQLitePersistenceSerivceTest
    {
        [Fact]
        public void Test()
        {
            var users = SampleUsers(10);
            var contextMock = new Mock<SQLiteDataContext>();

            //contextMock.Setup(x => x.Users).Returns
        }

        private static IEnumerable<User> SampleUsers(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(x => x.Email, x => x.Internet.Email())
                .RuleFor(x => x.Username, x => x.Name.FirstName())
                .RuleFor(x => x.Password, x => x.Hacker.Phrase());

            return faker.Generate(count);
        }

        private static IEnumerable<Account> SampleAccounts(int count, Guid userId)
        {
            var faker = new Faker<Account>()
                .RuleFor(x => x.Balance, x => (double)x.Finance.Amount())
                .RuleFor(x => x.Currency, x => x.Finance.Currency().ToString())
                .RuleFor(x => x.OwnerId, x => userId);

            return faker.Generate(count);
        }

        private static IEnumerable<Transaction> SampleTransactions(int count, Guid userId)
        {
            var faker = new Faker<Transaction>()
                .RuleFor(x => x.AccountID, x => userId)
                .RuleFor(x => x.Value, x => (double)x.Finance.Amount())
                .RuleFor(x => x.Date, x => x.Date.Recent());
            
            return faker.Generate(count);
        }
    }
}
