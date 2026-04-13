using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SalesUpds;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SalesUpds
{
    [InjectAsSingleton(typeof(ISalesUpdsNdsApiClient))]
    public class SalesUpdsNdsApiClient : BaseApiClient, ISalesUpdsNdsApiClient
    {
        public SalesUpdsNdsApiClient(
            IHttpRequestExecuter httpRequestExecuter, 
            IUriCreator uriCreator, 
            IAuditTracer auditTracer, 
            IAuthHeadersGetter authHeadersGetter, 
            IAuditHeadersGetter auditHeadersGetter, 
            ISettingRepository settingRepository,
            ILogger<SalesUpdsNdsApiClient> logger) 
            : base(
                httpRequestExecuter, 
                uriCreator, 
                auditTracer, 
                authHeadersGetter, 
                auditHeadersGetter, 
                settingRepository.Get("UpdsApiEndpoint"), 
                logger)
        {
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