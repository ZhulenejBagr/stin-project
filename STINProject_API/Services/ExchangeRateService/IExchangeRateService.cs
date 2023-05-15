namespace STINProject_API.Services.ExchangeRateService
{
    public interface IExchangeRateService
    {
        public double ExchangeCurrency(double value, string currencyCodeFrom, string currencyCodeTo);
        public bool IsCurrencySupported(string currencyCode);
    }
}