using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Spam.ApiClient.Abastractions.Dto.SmsSender;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.SmsSender;

namespace Moedelo.Spam.ApiClient.Legacy.SmsSender
{
    [InjectAsSingleton(typeof(ISmsSenderClient))]
    public class SmsSenderClient : BaseLegacyApiClient, ISmsSenderClient
    {
        public SmsSenderClient(
            IHttpRequestExecuter httpRequestExecuter, 
            IUriCreator uriCreator, 
            IAuditTracer auditTracer, 
            IAuditHeadersGetter auditHeadersGetter, 
            ISettingRepository settingRepository, 
            ILogger<SmsSenderClient> logger) 
            : base(
                httpRequestExecuter, 
                uriCreator, 
                auditTracer, 
                auditHeadersGetter,
                settingRepository.Get("smsServiceUrl"), 
                logger)
        {
        }

        public Task<List<SmsSendResponseDto>> SendSmsAsync(SmsSendRequestDto dto)
        {
            return PostAsync<SmsSendRequestDto, List<SmsSendResponseDto>>("/V2/SendSms", dto);
        }
    }
}