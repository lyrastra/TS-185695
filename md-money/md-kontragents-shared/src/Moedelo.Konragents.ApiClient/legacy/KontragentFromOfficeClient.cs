using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IKontragentFromOfficeClient))]
    internal sealed class KontragentFromOfficeClient : BaseLegacyApiClient, IKontragentFromOfficeClient
    {
        public KontragentFromOfficeClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<KontragentContactApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("KontragentsPrivateApiEndpoint"),
                logger)
        {
        }

        public Task<KontragentDto> GetAsync(int firmId, int userId, string inn)
        {
            if (string.IsNullOrEmpty(inn))
            {
                return Task.FromResult(default(KontragentDto));
            }

            return GetAsync<KontragentDto>("/FromOffice", new { firmId, userId, inn });
        }
    }
}
