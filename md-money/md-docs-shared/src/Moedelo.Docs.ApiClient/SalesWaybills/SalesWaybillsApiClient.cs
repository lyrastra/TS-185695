using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SalesWaybills;
using Moedelo.Docs.ApiClient.Abstractions.SalesWaybills.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SalesWaybills
{
    [InjectAsSingleton(typeof(ISalesWaybillsApiClient))]
    public class SalesWaybillsApiClient : BaseApiClient, ISalesWaybillsApiClient
    {
        public SalesWaybillsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesWaybillsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("WaybillsApiEndpoint"),
                logger)
        {
        }
        
        public Task<DataPageResponse<DocsSalesWaybillByCriteriaResponseDto>> GetByCriteriaAsync(DocsSalesWaybillsByCriteriaRequestDto criteria, int? companyId = null)
        {
            return PostAsync<DocsSalesWaybillsByCriteriaRequestDto, DataPageResponse<DocsSalesWaybillByCriteriaResponseDto>>(
                $"/api/v1/Sales/GetByCriteria?_companyId={companyId}", 
                criteria);
        }

        public async Task<IReadOnlyCollection<NdsSumDto>> GetNdsSumByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (!baseIds.Any())
            {
                return new List<NdsSumDto>();
            }

            var url = "/private/api/v1/Sales/GetNdsSumByBaseIds";
            var response = await PostAsync<IReadOnlyCollection<long>, DataResponse<NdsSumDto[]>>(url, baseIds);
            return response.Data;
        }
    }
}