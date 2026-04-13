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
using Moedelo.Requisites.ApiClient.Legacy.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IPatentApiClient))]
    internal sealed class PatentApiClient : BaseLegacyApiClient, IPatentApiClient
    {
        public PatentApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PatentApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public async Task<bool> IsAnyExists(FirmId firmId, UserId userId, int year)
        {
            var result = await GetAsync<DataResponseWrapper<bool>>("/Patent/IsAnyExists", new { firmId, userId, year })
                .ConfigureAwait(false);
            return result.Data;
        }

        public async Task<PatentWithoutAdditionalDataDto[]> GetWithoutAdditionalDataAsync(FirmId firmId, UserId userId, int? year)
        {
            var result = await GetAsync<ListResponseWrapper<PatentWithoutAdditionalDataDto>>("/Patent/GetWithoutAdditionalData", new { firmId, userId, year })
                .ConfigureAwait(false);
            return result.Items;
        }

        public async Task<PatentWithoutAdditionalDataDto> GetWithoutAdditionalDataByIdAsync(FirmId firmId, UserId userId, long id)
        {
            var result = await GetAsync<DataResponseWrapper<PatentWithoutAdditionalDataDto>>("/Patent/GetWithoutAdditionalDataById", new { firmId, userId, id })
                .ConfigureAwait(false);
            return result.Data;
        }

        public async Task<IReadOnlyCollection<PatentWithoutAdditionalDataDto>> GetWithoutAdditionalDataByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> ids)
        {
            var result = await PostAsync<IReadOnlyCollection<long>, ListResponseWrapper<PatentWithoutAdditionalDataDto>>($"/Patent/GetByIds?firmId={firmId}&userId={userId}", ids)
                .ConfigureAwait(false);
            return result.Items;
        }

        public Task TurnOffNotificationsByFirmIdAsync(FirmId firmId, UserId userId)
        {
            return PostAsync($"/Patent/TurnOffNotificationsByFirmId?firmId={firmId}&userId={userId}");
        }
    }
}