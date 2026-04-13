using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Waybills.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Docs.ApiClient.legacy.Purchases.Waybills
{
    [InjectAsSingleton(typeof(IPurchasesWaybillApiClient))]
    public class PurchasesWaybillApiClient : BaseLegacyApiClient, IPurchasesWaybillApiClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue docsApiEndpoint;

        public PurchasesWaybillApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesWaybillApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
            docsApiEndpoint = settingRepository.Get("DocsApiEndpoint");
            this.httpRequestExecuter = httpRequestExecuter;
        }

        // todo: вынести в другой клиент, наследованный от BaseLegacyApiClient (сейчас метод не перехватывается аудитом)
        public async Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<PaidSumDto>();
            }

            var uri = $"{docsApiEndpoint.Value}/PurchasesWaybills/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}";
            var response = await httpRequestExecuter.PostAsync(uri, baseIds.ToUtf8JsonContent());
            return response.FromJsonString<List<PaidSumDto>>();
        }

        public Task<PurchasesWaybillDto> SaveAsync(FirmId firmId, UserId userId, PurchasesWaybillSaveRequestDto dto)
        {
            var uri = $"/api/v1/purchases/waybill?firmId={firmId}&userId={userId}";
            return PostAsync<PurchasesWaybillSaveRequestDto, PurchasesWaybillDto>(uri, dto);
        }
    }
}