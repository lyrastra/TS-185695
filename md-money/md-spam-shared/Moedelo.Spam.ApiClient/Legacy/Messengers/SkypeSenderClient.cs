using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Spam.ApiClient.Abastractions.Dto.Messengers;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.Messengers;

namespace Moedelo.Spam.ApiClient.Legacy.Messengers
{
    [InjectAsSingleton(typeof(ISkypeSenderClient))]
    internal sealed class SkypeSenderClient : BaseLegacyApiClient, ISkypeSenderClient
    {
        public SkypeSenderClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SkypeSenderClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("mailServiceUrl"),
                logger)
        {
        }

        public Task SendAsync(SkypeSendOptionsDto request) => PostAsync("/Skype/Send", request);
    }
}