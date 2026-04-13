using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.legacy.Purchases.Upds
{
    [InjectAsSingleton(typeof(IPurchasesUpdApiClient))]
    public class PurchasesUpdApiClient : BaseLegacyApiClient, IPurchasesUpdApiClient
    {
        public PurchasesUpdApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesUpdApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("DocsApiEndpoint"),
                logger)
        {}

        public Task<List<PurchasesUpdWithItemsDto>> GetWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<PurchasesUpdWithItemsDto>());
            }

            var uri = $"/Upd/GetByBaseIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<PurchasesUpdWithItemsDto>>(uri, baseIds);
        }

        public Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<PaidSumDto>());
            }

            var uri = $"/Upd/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<PaidSumDto>>(uri, baseIds);
        }

        public Task<PurchasesUpdExternalDto> SaveAsync(FirmId firmId, UserId userId, PurchasesUpdSaveRequestDto dto)
        {
            var uri = $"/api/v1/Purchases/Upd?firmId={firmId}&userId={userId}";
            return PostAsync<PurchasesUpdSaveRequestDto, PurchasesUpdExternalDto>(uri, dto);
        }
        
        public Task ReprovideByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/Upd/ReprovideByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }
    }
}