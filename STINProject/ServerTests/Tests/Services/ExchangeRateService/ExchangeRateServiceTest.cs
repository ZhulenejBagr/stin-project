﻿using Moq;
using STINProject.Server.Services.ExchangeRateService;
using Xunit;

namespace ServerTests.Tests.Services.ExchangeRateService
{
    [TestCaseOrderer("ServerTests.PriorityOrderer", "ServerTests")]
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
            var firstIndex = new ExchangeRateRecordIndex { ExchangeFromCode = "GBP", ExchangeToCode = "CZK" };
            var secondIndex = new ExchangeRateRecordIndex { ExchangeFromCode = "USD", ExchangeToCode = "CZK" };

            var firstRecord = _fixture.Document.ExchangeRecords[firstIndex];
            var secondRecord = _fixture.Document.ExchangeRecords[secondIndex];
            var inputValue = 50;
            var expectedValue = firstRecord.ExchangeRate / firstRecord.Quantity * secondRecord.Quantity / secondRecord.ExchangeRate * inputValue;
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
