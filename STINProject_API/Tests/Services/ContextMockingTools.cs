using Microsoft.EntityFrameworkCore;
using Bogus;
using Moq;
using STINProject_API.Services.PersistenceService.Model;
using STINProject_API.Services.PersistenceService.Models;

namespace STINProject_API.Tests.Services
{
    public static class ContextMockingTools
    {
        public static IEnumerable<User> SampleUsers(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(x => x.Email, x => x.Internet.Email())
                .RuleFor(x => x.Username, x => x.Name.FirstName())
                .RuleFor(x => x.Password, x => x.Hacker.Phrase())
                .RuleFor(x => x.UserId, x => Guid.NewGuid());

            return faker.Generate(count);
        }

        public static IEnumerable<Account> SampleAccounts(int count, Guid userId)
        {
            var faker = new Faker<Account>()
                .RuleFor(x => x.Balance, x => (double)x.Finance.Amount())
                .RuleFor(x => x.Currency, x => x.Finance.Currency().ToString())
                .RuleFor(x => x.OwnerId, x => userId)
                .RuleFor(x => x.AccountId, x => Guid.NewGuid());

            return faker.Generate(count);
        }

        public static IEnumerable<Transaction> SampleTransactions(int count, Guid accountId)
        {
            var faker = new Faker<Transaction>()
                .RuleFor(x => x.AccountID, x => accountId)
                .RuleFor(x => x.Value, x => (double)x.Finance.Amount())
                .RuleFor(x => x.Date, x => x.Date.Recent())
                .RuleFor(x => x.TransactionID, x => Guid.NewGuid());

            return faker.Generate(count);
        }

        // https://stackoverflow.com/questions/31349351/how-to-add-an-item-to-a-mock-dbset-using-moq
        public static DbSet<T> GetQueryableMockDbSet<T>(IList<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}
