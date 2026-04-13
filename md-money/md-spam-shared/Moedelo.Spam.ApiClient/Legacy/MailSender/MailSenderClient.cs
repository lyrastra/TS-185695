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
using Moedelo.Spam.ApiClient.Abastractions.Dto.MailSender;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.MailSender;

namespace Moedelo.Spam.ApiClient.Legacy.MailSender
{
    [InjectAsSingleton(typeof(IMailSenderClient))]
    internal sealed class MailSenderClient : BaseLegacyApiClient, IMailSenderClient
    {
        private new readonly HttpQuerySetting DefaultSetting = new HttpQuerySetting(TimeSpan.FromSeconds(100), true);

        public MailSenderClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MailSenderClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("mailServiceUrl"),
                logger)
        {
        }

        public Task<MailResponseDto> SendAsync(EmailDto request) =>
            PostAsync<EmailDto, MailResponseDto>("/SendEmail", request, setting: DefaultSetting);
        
        public Task<BaseEmailResponseDto> FillAndSendEmailAsync(EmailParamsV2Dto request)
        {
            return PostAsync<EmailParamsV2Dto, BaseEmailResponseDto>("/V2/FillAndSendEmailAsync", request, setting: DefaultSetting);
        }

        public Task<bool> HasMailByTrackingCodeAsync(Guid trackingCode, CancellationToken cancellationToken)
        {
            return GetAsync<bool>("/V2/HasMailByTrackingCode", new { trackingCode }, cancellationToken: cancellationToken);
        }
    }
}