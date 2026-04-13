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
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Waybills.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Docs.ApiClient.legacy.Sales.Waybills
{
    [InjectAsSingleton(typeof(ISalesWaybillApiClient))]
    public class SalesWaybillApiClient : BaseLegacyApiClient, ISalesWaybillApiClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue docsApiEndpoint;

        public SalesWaybillApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesWaybillApiClient> logger)
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

            var uri = $"{docsApiEndpoint.Value}/SalesWaybills/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}";
            var response = await httpRequestExecuter.PostAsync(uri, baseIds.ToUtf8JsonContent()).ConfigureAwait(false);
            return response.FromJsonString<List<PaidSumDto>>();
        }

        public async Task<SalesWaybillDto> GetByBaseIdAsync(FirmId firmId, UserId userId, long baseId)
        {
            try
            {
                var uri = $"/api/v1/sales/waybill/{baseId}?firmId={firmId}&userId={userId}";
                return await GetAsync<SalesWaybillDto>(uri);
            }
            catch (HttpRequestResponseStatusException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }
        
        public Task<SalesWaybillDto> SaveAsync(FirmId firmId, UserId userId, SalesWaybillSaveRequestDto dto)
        {
            var uri = $"/api/v1/sales/waybill?firmId={firmId}&userId={userId}";
            return PostAsync<SalesWaybillSaveRequestDto, SalesWaybillDto>(uri, dto);
        }
    }
}