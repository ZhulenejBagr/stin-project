using Moq;
using ServerTests.Tests.Services.PersistenceService;
using STINProject.Server.Services.ExchangeRateService;
using STINProject.Server.Services.PersistenceService;
using STINProject.Server.Services.PersistenceService.Models;
using STINProject.Server.Services.TransactionService;

namespace ServerTests.Tests.Services.TransactionService
{
    public class TransactionFixture
    {
        private readonly IPersistenceService _persistenceService;
        private readonly IExchangeRateService _exchangeRateService;
        public readonly ITransactionService ServiceUnderTest;

        public IList<User> Users { get; init; }
        public IList<Account> Accounts { get; init; }
        public IList<Transaction> Transactions { get; init; }

        public ExchangeRateDocument ExchangeRateDocument { get; init; }

        public TransactionFixture() 
        {
            ExchangeRateDocument = new ExchangeRateDocument(DateTime.Today,
                new string[]
                {
                    "CZK",
                    "USD",
                    "GBP"
                },
                new Dictionary<ExchangeRateRecordIndex, ExchangeRateRecord>
                {
                    {
                        new ExchangeRateRecordIndex { ExchangeFromCode = "CZK", ExchangeToCode = "USD" },
                        new ExchangeRateRecord { Country = "Amerika", CountryCode = "USA", Currency = "dolar", ExchangeRate = 22.2, Quantity = 1 }
                    },
                    {
                        new ExchangeRateRecordIndex { ExchangeFromCode = "CZK", ExchangeToCode = "GBP" },
                        new ExchangeRateRecord { Country = "Velká Británie", CountryCode = "UK", Currency = "libra", ExchangeRate = 25.9, Quantity = 1 }
                    }
                });


            Users = ContextMockingTools.SampleUsers(2).ToList();
            Accounts = ContextMockingTools.SampleAccounts(2, Users[0].UserId, ExchangeRateDocument.SupportedCurrecyCodes).ToList();
            Transactions = ContextMockingTools.SampleTransactions(2, Accounts[0].AccountId).ToList();

            var mockPersistenceService = new Mock<IPersistenceService>();
                
            mockPersistenceService
                .Setup(x => x.GetAccounts(It.IsAny<Guid>()))
                .Returns((Guid userId) => Accounts.Where(x => x.OwnerId == userId));
            mockPersistenceService
                .Setup(x => x.AddTransaction(It.IsAny<Transaction>()))
                .Returns((Transaction transaction) => true);


            var mockExchangeRateService = new Mock<IExchangeRateService>();
            mockExchangeRateService
                .Setup(x => x.IsCurrencySupported(It.IsAny<string>()))
                .Returns((string code) => ExchangeRateDocument.SupportedCurrecyCodes.Contains(code));
            mockExchangeRateService
                .Setup(x => x.ExchangeCurrency(It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((double value, string from, string to) =>
                {
                    var index = new ExchangeRateRecordIndex { ExchangeFromCode = from, ExchangeToCode = to };
                    var record = ExchangeRateDocument.ExchangeRecords.FirstOrDefault(x => x.Key == index);

                    return value * record.Value.ExchangeRate / record.Value.Quantity;
                });

            ServiceUnderTest = new SimpleTransactionService(mockExchangeRateService.Object, mockPersistenceService.Object);
        }
    }
}
