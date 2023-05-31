using Moq;
using Moq.Protected;
using STINProject.Server.Services.ExchangeRateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerTests.Tests.Services.ExchangeRateService
{
    public class ExchangeRateDocumentGetterFixture : IDisposable
    {
        public readonly ExchangeRateDocumentGetter ServiceUnderTest;

        public readonly string ExampleDocument1 = "31.05.2023 #104\nzemě|měna|množství|kód|kurz\nAustrálie|dolar|1|AUD|14,394\nBrazílie|real|1|BRL|4,387\nBulharsko|lev|1|BGN|12,141";
        public readonly string ExternalDocumentUri = "https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt";

        public string[] ExampleDocument1Split => ExampleDocument1.Split('\n');
        public ExchangeRateDocumentGetterFixture()
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(ExampleDocument1)
                });

            ServiceUnderTest = new ExchangeRateDocumentGetter(new HttpClient(mockMessageHandler.Object));
        }

        public void Dispose()
        {
        }
    }
}
