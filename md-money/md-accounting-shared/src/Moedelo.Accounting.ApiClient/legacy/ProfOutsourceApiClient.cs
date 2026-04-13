using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IProfOutsourceApiClient))]
    public class ProfOutsourceApiClient : BaseLegacyApiClient, IProfOutsourceApiClient
    {
        public ProfOutsourceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ProfOutsourceApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("ProfOutsourceApiEndpoint"),
                logger)
        {
        }

        public Task<ProfOutsourceContextDto> GetOutsourceContextAsync(int firmId, int userId)
        {
            return GetAsync<ProfOutsourceContextDto>("/OutsourceContext", new { firmId, userId});
        }
    }
}