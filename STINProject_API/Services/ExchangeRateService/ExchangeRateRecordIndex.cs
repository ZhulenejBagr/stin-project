﻿namespace STINProject_API.Services.ExchangeRateService
{
    public record ExchangeRateRecordIndex
    {
        public string ExchangeFromCode { get; set; }
        public string ExchangeToCode { get; set; }
    }
}
