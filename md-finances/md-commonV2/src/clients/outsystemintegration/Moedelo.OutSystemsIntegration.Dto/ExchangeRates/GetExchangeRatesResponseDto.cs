using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.ExchangeRates
{
    public class GetExchangeRatesResponseDto
    {
        public DateTime Date { get; set; }

        public ExchangeRateValueDto[] Values { get; set; }
    }
}
