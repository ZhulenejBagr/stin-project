using System.Text;

namespace STINProject.Server.Services.ExchangeRateService
{
    public class SimpleExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRateGetter _documentGetter;
        public SimpleExchangeRateService(IExchangeRateGetter documentGetter)
        {
            _documentGetter = documentGetter;
        }
        public double ExchangeCurrency(double exchangeValue, string currencyCodeFrom, string currencyCodeTo)
        {
            var doc = _documentGetter.GetExchangeRateDocument();
            var index = new ExchangeRateRecordIndex { ExchangeFromCode = currencyCodeFrom, ExchangeToCode = currencyCodeTo };
            var invertedIndex = new ExchangeRateRecordIndex { ExchangeFromCode = currencyCodeTo, ExchangeToCode = currencyCodeFrom };

            if (doc.ExchangeRecords.TryGetValue(index, out ExchangeRateRecord value))
            {
                var record = value;
                return record.ExchangeRate / record.Quantity * exchangeValue;
            }
            else if (doc.ExchangeRecords.TryGetValue(invertedIndex, out ExchangeRateRecord val))
            {
                var record = val;
                return record.Quantity / record.ExchangeRate * exchangeValue;
            }
            else
            {
                foreach (var frecord in doc.ExchangeRecords.Where(x => x.Key.ExchangeFromCode == currencyCodeTo))
                {
                    foreach (var srecord in doc.ExchangeRecords.Where(x => x.Key.ExchangeFromCode == currencyCodeFrom))
                    {
                        if (frecord.Key.ExchangeToCode == srecord.Key.ExchangeToCode)
                        {
                            return (srecord.Value.ExchangeRate / srecord.Value.Quantity) * (frecord.Value.Quantity / frecord.Value.ExchangeRate) * exchangeValue;
                        }
                    }
                }

                return double.NaN;
            }
        }

        public bool IsCurrencySupported(string currencyCode)
        {
            return _documentGetter.GetExchangeRateDocument().SupportedCurrecyCodes.Contains(currencyCode);
        }
    }
}
