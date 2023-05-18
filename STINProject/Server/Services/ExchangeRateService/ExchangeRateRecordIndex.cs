using System.Diagnostics.CodeAnalysis;

namespace STINProject.Server.Services.ExchangeRateService
{
    [ExcludeFromCodeCoverage]
    public record ExchangeRateRecordIndex
    {
        public string ExchangeFromCode { get; set; }
        public string ExchangeToCode { get; set; }
    }
}
