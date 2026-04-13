using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SalesUpds;
using Moedelo.Docs.ApiClient.Abstractions.SalesUpds.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SalesUpds
{
    // todo: подумать как перетащить реализацию в SalesUpdsApiClient. Проблема подменять на лету EndpointSetting
    [InjectAsSingleton(typeof(ISalesUpdsCoreApiClient))]
    public class SalesUpdsCoreApiClient : BaseApiClient, ISalesUpdsCoreApiClient
    {
        public SalesUpdsCoreApiClient(
            IHttpRequestExecuter httpRequestExecuter, 
            IUriCreator uriCreator, 
            IAuditTracer auditTracer, 
            IAuthHeadersGetter authHeadersGetter, 
            IAuditHeadersGetter auditHeadersGetter, 
            ISettingRepository settingRepository,
            ILogger<SalesUpdsCoreApiClient> logger) 
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

        public Task<DataPageResponse<DocsSalesUpdByCriteriaResponseDto>> GetByCriteriaAsync(DocsSalesUpdsByCriteriaRequestDto criteria, int? companyId = null)
        {
            return PostAsync<DocsSalesUpdsByCriteriaRequestDto, DataPageResponse<DocsSalesUpdByCriteriaResponseDto>>(
                $"/api/v1/Sales/GetByCriteria?_companyId={companyId}", 
                criteria);
        }
    }
}