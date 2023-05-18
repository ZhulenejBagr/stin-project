using System.Globalization;
using Hangfire;
using Hangfire.SqlServer;

namespace STINProject.Server.Services.ExchangeRateService
{
    public class ExchangeRateGetter : IExchangeRateGetter
    {
        private static readonly string _externalDocumentURL =
           "https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt";
        private static readonly string _implicitCurrencyCode = "CZK";
        private static readonly CultureInfo _localCulture = new("cs-CZ");

        private ExchangeRateDocument _savedDocument;

        public ExchangeRateGetter()
        {
            GetDocumentAndSave();
            var times = Enumerable.Range(0, 10).Select(x => $"{30 + x} 14 * * *").ToArray();
            for (var i = 0; i < times.Length; i++)
            {
                RecurringJob.AddOrUpdate($"get-exchange-doc-{i}", () => GetDocumentAndSave(), times[i]);
            }
        }
        private static async Task<string[]> RequestDocument()
        {
            var request = new HttpClient();
            var website = await request.GetStringAsync(_externalDocumentURL);

            var lines = website.Split("\n");

            return lines;
        }

        public void GetDocumentAndSave()
        {
            _savedDocument = ParseExchangeDocument(RequestDocument().Result);
        }

        private static ExchangeRateDocument ParseExchangeDocument(string[] document)
        {
            var issuedOn = DateTime.ParseExact(document[0].Split(' ')[0], "dd.mm.yyyy", _localCulture);
            var records = new Dictionary<ExchangeRateRecordIndex, ExchangeRateRecord>();
            var currencies = new List<string>() { "CZK" };

            var dataLines = document[2..^2];
            foreach (var line in dataLines)
            {
                var split = line.Split('|');
                if (!currencies.Contains(split[3]))
                {
                    currencies.Add(split[3]);
                }

                var index = new ExchangeRateRecordIndex
                {
                    ExchangeFromCode = split[3],
                    ExchangeToCode = _implicitCurrencyCode
                };
                var record = new ExchangeRateRecord
                {
                    Country = split[0],
                    Currency = split[1],
                    Quantity = int.Parse(split[2]),
                    CountryCode = split[3],
                    ExchangeRate = double.Parse(split[4])
                };
                records.Add(index, record);
            }

            return new ExchangeRateDocument(issuedOn, currencies, records);
        }

        public ExchangeRateDocument GetExchangeRateDocument()
        {
            return _savedDocument;
        }
    }
}
