using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Spam.ApiClient.Abastractions.Dto.Sms;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.Sms;

namespace Moedelo.Spam.ApiClient.Sms
{
    [InjectAsSingleton(typeof(ISmsApiClient))]
    internal sealed class SmsApiClient(IHttpRequestExecuter httpRequestExecutor, IUriCreator uriCreator, IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter, IAuditHeadersGetter auditHeadersGetter, ISettingRepository settingRepository,
        ILogger<SmsApiClient> logger, string auditTypeName = null) 
            : BaseApiClient(httpRequestExecutor, uriCreator, auditTracer, authHeadersGetter, auditHeadersGetter,
                settingRepository.Get("SmsApiEndpoint"), logger, auditTypeName),
            ISmsApiClient
    {
        private static readonly HttpQuerySetting ExtendedTimeoutQuerySetting = new(TimeSpan.FromSeconds(120));

        public Task<SendSmsResponseDto> SendAsync(SendSmsRequestDto request, CancellationToken cancellationToken)
        {
            return PostAsync<SendSmsRequestDto, SendSmsResponseDto>(
                "/api/v1/sms/send",
                request,
                setting: ExtendedTimeoutQuerySetting,
                cancellationToken: cancellationToken);
        }
    }
}