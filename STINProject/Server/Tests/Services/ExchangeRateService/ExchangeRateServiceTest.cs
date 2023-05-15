using Moq;
using STINProject.Server.Services.ExchangeRateService;
using Xunit;

namespace STINProject.Server.Tests.Services.ExchangeRateService
{
    [TestCaseOrderer("STINProject_API.Tests.PriorityOrderer", "STINProject_API")]
    public class ExchangeRateServiceTest : IClassFixture<ExchangeRateFixture>
    {
        private readonly ExchangeRateFixture _fixture;

        public ExchangeRateServiceTest(ExchangeRateFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ExchangeCurrency_ShouldExchangeCurrency_WhenDirectExchangeExists()
        {
            var record = _fixture.Document.ExchangeRecords.First();
            var inputValue = 2;
            var expectedValue = record.Value.ExchangeRate / record.Value.Quantity * inputValue;

            Assert.Equal(expectedValue, _fixture.ServiceUnderTest.ExchangeCurrency(inputValue, record.Key.ExchangeFromCode, record.Key.ExchangeToCode));
        }

        [Fact]
        public void ExchangeCurrency_ShouldExchangeCurrency_WhenReverseExchangeExists()
        {
            var record = _fixture.Document.ExchangeRecords.First();
            var inputValue = 50;
            var expectedValue = record.Value.Quantity / record.Value.ExchangeRate * inputValue;

            Assert.Equal(expectedValue, _fixture.ServiceUnderTest.ExchangeCurrency(inputValue, record.Key.ExchangeToCode, record.Key.ExchangeFromCode));
        }

        [Fact]
        public void ExchangeCurrency_ShouldExchangeCurrency_WhenAlternateExchangeExists()
        {
            var recordIndex = new ExchangeRateRecordIndex { ExchangeFromCode = "GBP", ExchangeToCode = "USD" };
            var firstIndex = new ExchangeRateRecordIndex { ExchangeFromCode = "CZK", ExchangeToCode = "GBP" };
            var secondIndex = new ExchangeRateRecordIndex { ExchangeFromCode = "CZK", ExchangeToCode = "USD" };

            var firstRecord = _fixture.Document.ExchangeRecords[firstIndex];
            var secondRecord = _fixture.Document.ExchangeRecords[secondIndex];
            var inputValue = 50;
            var expectedValue = firstRecord.Quantity / firstRecord.ExchangeRate * secondRecord.ExchangeRate / secondRecord.Quantity * inputValue;
            Assert.Equal(expectedValue, _fixture.ServiceUnderTest.ExchangeCurrency(inputValue, firstRecord.CountryCode, secondRecord.CountryCode));
        }

        [Fact]
        public void IsCurrencySupported_ShouldBeTrue_WhenCurrencyIsInDocument()
        {
            var currency = _fixture.Document.SupportedCurrecyCodes.First();
            Assert.True(_fixture.ServiceUnderTest.IsCurrencySupported(currency));
        }

        [Fact]
        public void IsCurrencySupported_ShouldBeFalse_WhenCurrencyNotInDocument()
        {
            var currency = "NAN";
            Assert.False(_fixture.ServiceUnderTest.IsCurrencySupported(currency));
        }
    }
}
