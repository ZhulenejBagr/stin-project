using STINProject.Server.Services.ExchangeRateService;
using System.Globalization;

namespace ServerTests.Tests.Services.ExchangeRateService
{
    public class ExchangeRateDocumentGetterTest : IClassFixture<ExchangeRateDocumentGetterFixture>
    {
        private readonly ExchangeRateDocumentGetterFixture _fixture;

        public ExchangeRateDocumentGetterTest(ExchangeRateDocumentGetterFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public void ParseExchangeDocument_ShouldParsePropertly_WhenCorrectDocument()
        {
            var parse = _fixture.ServiceUnderTest.ParseExchangeDocument(_fixture.ExampleDocument1Split);

            Assert.NotNull(parse);
            Assert.Equal(DateTime.ParseExact("31.05.2023", "dd.mm.yyyy", new CultureInfo("cs-CZ")), parse.IssuedOn);
            Assert.Equal(new string[] { "CZK", "AUD", "BRL", "BGN" }, parse.SupportedCurrecyCodes);
        }

    }
}
