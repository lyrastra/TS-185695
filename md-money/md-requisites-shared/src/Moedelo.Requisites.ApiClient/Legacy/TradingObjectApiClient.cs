using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Legaсy
{
    [InjectAsSingleton(typeof(ITradingObjectApiClient))]
    internal sealed class TradingObjectApiClient : BaseLegacyApiClient, ITradingObjectApiClient
    {
        public TradingObjectApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<TradingObjectApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public async Task<TradingObjectDto> GetByIdAsync(FirmId firmId, UserId userId, int tradingObjectId)
        {
            var uri = $"/TradingObject/GetTradingObject?firmId={firmId}&userId={userId}&id={tradingObjectId}";
            var response = await GetAsync<Legacy.Wrappers.DataResponseWrapper<TradingObjectDto>>(uri).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<TradingObjectShortDto[]> GetShortAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/TradingObject/GetShortTradingObjectsByFirmId?firmId={firmId}&userId={userId}";
            var response = await GetAsync<Legacy.Wrappers.ListResponseWrapper<TradingObjectShortDto>>(uri)
                .ConfigureAwait(false);
            return response.Items ?? Array.Empty<TradingObjectShortDto>();
        }

        public async Task<TradingObjectDto[]> GetByCriteriaAsync(FirmId firmId, UserId userId, TradingObjectCriteriaDto criteria)
        {
            var uri = $"/TradingObject/GetTradingObjectListByCritery";
            var response = await GetAsync<Legacy.Wrappers.ListResponseWrapper<TradingObjectDto>>(uri,
                new
                {
                    firmId,
                    userId,
                    criteria.Year,
                    criteria.IsActual
                }).ConfigureAwait(false);
            return response.Items ?? Array.Empty<TradingObjectDto>();
        }
    }
}