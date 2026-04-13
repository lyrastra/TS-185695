using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Konragents.ApiClient.legacy.models;

namespace Moedelo.Konragents.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IKontragentKppsApiClient))]
    internal sealed class KontragentKppsApiClient : BaseLegacyApiClient, IKontragentKppsApiClient
    {
        public KontragentKppsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<KontragentKppsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("KontragentsPrivateApiEndpoint"),
                logger)
        {
        }

        public async Task<KontragentKppDto> GetByKontragentAsync(FirmId firmId, UserId userId, int kontragentId,
            DateTime date)
        {
            var result = await GetAsync<DataResult<KontragentKppDto>>("/Kpp/GetByKontragentAndDate",
                new {firmId, userId, kontragentId, date}).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<IReadOnlyList<KontragentKppDto>> GetByKontragentIdsAsync(FirmId firmId, UserId userId,
            KontragentKppsRequestDto request)
        {
            var result =
                await PostAsync<KontragentKppsRequestDto, DataResult<IReadOnlyList<KontragentKppDto>>>(
                    $"/Kpp/GetByKontragentIds?firmId={firmId}&userId={userId}", request);

            return result.Data;
        }

        public Task<long> SaveAsync(FirmId firmId, UserId userId, KontragentKppDto kpp)
        {
            return PostAsync<KontragentKppDto, long>($"/KppV2/Save?firmId={firmId}&userId={userId}", kpp);
        }

        public async Task<IList<KontragentKppDto>> GetByKontragentAsync(FirmId firmId, UserId? userId, int kontragentId)
        {
            var result = await GetAsync<DataResult<IList<KontragentKppDto>>>("/Kpp/GetByKontragent", new { firmId, userId, kontragentId }).ConfigureAwait(false);
            return result.Data;
        }

    }
}