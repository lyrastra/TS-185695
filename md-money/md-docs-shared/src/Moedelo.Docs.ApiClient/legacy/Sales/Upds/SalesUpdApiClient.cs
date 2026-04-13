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
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Docs.ApiClient.legacy.Sales.Upds
{
    [InjectAsSingleton(typeof(ISalesUpdApiClient))]
    public class SalesUpdApiClient : BaseLegacyApiClient, ISalesUpdApiClient
    {
        public SalesUpdApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesUpdApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("DocsApiEndpoint"),
                logger)
        {
        }
        
        public Task<List<SalesUpdWithItemsDto>> GetWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesUpdWithItemsDto>());
            }
            
            var uri = $"/SalesUpd/GetWithItems?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<SalesUpdWithItemsDto>>(uri, baseIds);
        }


        public async Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<PaidSumDto>();
            }

            try
            {
                var uri = $"/SalesUpd/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}";
                return await PostAsync<IReadOnlyCollection<long>, List<PaidSumDto>>(uri, baseIds);
            }
            catch (HttpRequestResponseStatusException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<PaidSumDto>();
                }
                throw;
            }
        }

        public Task<List<SalesUpdWithItemsDto>> GetWithItemsAsync(int firmId, int userId, List<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesUpdWithItemsDto>());
            }

            var uri = $"/SalesUpd/GetWithItems?firmId={firmId}&userId={userId}";
            return PostAsync<List<long>, List<SalesUpdWithItemsDto>>(uri, baseIds);
        }

        public Task<SalesUpdExternalDto> SaveAsync(FirmId firmId, UserId userId, SalesUpdSaveRequestDto dto)
        {
            return PostAsync<SalesUpdSaveRequestDto, SalesUpdExternalDto>($"/api/v1/sales/Upd?firmId={firmId}&userId={userId}", dto);
        }
    }
}