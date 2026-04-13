using System;

namespace Moedelo.Money.Business.ExchangeRates.ApiClient
{
    internal sealed class GetExchangeRatesResponseDto
    {
        public DateTime Date { get; set; }

        public ExchangeRateValueDto[] Values { get; set; }
    }
}