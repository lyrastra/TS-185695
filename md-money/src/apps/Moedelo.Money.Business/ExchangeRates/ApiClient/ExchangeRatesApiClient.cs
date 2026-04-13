using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Money.Business.ExchangeRates.ApiClient
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/8520202d9f199bc84acf6124959020ee6101ec65/src/clients/outsystemintegration/Moedelo.OutSystemsIntegration.Client/ExchangeRates/ExchangeRatesClient.cs#L14
    /// Если клиент понадобится в другом месте, перенести в shared
    /// </summary>
    [InjectAsSingleton(typeof(ExchangeRatesApiClient))]
    internal sealed class ExchangeRatesApiClient : BaseApiClient
    {
        public ExchangeRatesApiClient(
            IHttpRequestExecuter httpRequestExecuter, 
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ExchangeRatesApiClient> logger)
            : base(
                httpRequestExecuter, 
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("ExchangeRatesApiEndpoint"), 
                logger)
        {
        }

        public Task<GetExchangeRatesResponseDto[]> GetAsync(DateTime[] dates)
        {
            var request = new GetExchangeRatesRequestDto { Dates = dates };
            return PostAsync<GetExchangeRatesRequestDto, GetExchangeRatesResponseDto[]>("/V2/GetExchangeRates", request);
        }

        private class GetExchangeRatesRequestDto
        {
            public DateTime[] Dates { get; set; }
        }
    }
}