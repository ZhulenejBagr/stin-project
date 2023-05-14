namespace STINProject_API.Services.ExchangeRateService
{
    public class ExchangeRateRecord
    {
        public string Country { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public string CountryCode { get; set; }
        public double ExchangeRate { get; set; }
    }
}
