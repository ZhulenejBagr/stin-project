using Moq;
using STINProject.Server.Services.ExchangeRateService;

namespace ServerTests.Tests.Services.ExchangeRateService
{
    public class ExchangeRateFixture : IDisposable
    {
        public IExchangeRateService ServiceUnderTest { get; private set; }
        public ExchangeRateDocument Document { get; private set; }
        public ExchangeRateFixture()
        {
            var mockGetter = new Mock<IExchangeRateGetter>();
            var doc = GetMockDocument();
            mockGetter.Setup(x => x.GetExchangeRateDocument()).Returns(doc);
            ServiceUnderTest = new SimpleExchangeRateService(mockGetter.Object);
            Document = GetMockDocument();
        }

        private ExchangeRateDocument GetMockDocument()
        {
            var currencies = new List<string> { "CZK", "USD", "GBP" };
            var countries = new List<string> { "USA", "Velká Británie" };

            var records = new Dictionary<ExchangeRateRecordIndex, ExchangeRateRecord>
            {
                {
                    new ExchangeRateRecordIndex { ExchangeFromCode = currencies[0], ExchangeToCode = currencies[1] },
                    new ExchangeRateRecord { Country = countries[0], CountryCode = currencies[1], ExchangeRate = 22.1, Currency = "dolar", Quantity = 1 }
                },
                {
                    new ExchangeRateRecordIndex { ExchangeFromCode = currencies[0], ExchangeToCode = currencies[2] },
                    new ExchangeRateRecord { Country = countries[1], CountryCode = currencies[2], ExchangeRate = 27.1, Currency = "libra", Quantity = 1 }
                }
            };

            var issued = DateTime.Now;

            return new ExchangeRateDocument(issued, currencies, records);
        }
        public void Dispose()
        {
        }
    }
}
