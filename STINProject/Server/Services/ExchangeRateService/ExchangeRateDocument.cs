using STINProject.Server.Services.ExchangeRateService;

namespace STINProject.Server.Services.ExchangeRateService
{
    public class ExchangeRateDocument
    {
        public bool Valid { get; private set; }
        public DateTime IssuedOn { get; private set; }
        public IEnumerable<string> SupportedCurrecyCodes { get; private set; }
        public IDictionary<ExchangeRateRecordIndex, ExchangeRateRecord> ExchangeRecords { get; private set; }

        public ExchangeRateDocument(DateTime issuedOn, IEnumerable<string> supportedCurrencyCodes, IDictionary<ExchangeRateRecordIndex, ExchangeRateRecord> exchangeRecords)
        {
            IssuedOn = issuedOn;
            SupportedCurrecyCodes = supportedCurrencyCodes;
            ExchangeRecords = exchangeRecords;
            Valid = true;
        }

        public ExchangeRateDocument(bool valid)
        {
            Valid = valid;
        }
    }
}
