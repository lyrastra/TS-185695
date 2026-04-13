using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SalesBills;
using Moedelo.Docs.ApiClient.Abstractions.SalesBills.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SalesBills
{
    [InjectAsSingleton(typeof(ISalesBillsApiClient))]
    public class SalesBillsApiClient : BaseApiClient, ISalesBillsApiClient
    {
        public SalesBillsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesBillsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("BillsApiEndpoint"),
                logger)
        {
        }

        public async Task<List<SalesBillDto>> GetByBaseIdsAsync(IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return new List<SalesBillDto>();
            }
            
            var response = await PostAsync<IReadOnlyCollection<long>, DataResponse<List<SalesBillDto>>>("/api/v1/Sales/GetByBaseIds", ids);
            return response.Data;
        }

        public async Task<DataPageResponse<SalesBillByCriteriaResponseDto>> GetBillByCriteriaAsync(SalesBillByCriteriaRequestDto criteria)
        {
            var response =
                await PostAsync<SalesBillByCriteriaRequestDto, DataPageResponse<SalesBillByCriteriaResponseDto>>(
                    "/api/v1/Sales/GetByCriteria", criteria);

            return response;
        }
    }
}