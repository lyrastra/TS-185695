using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpamV2.Dto.MailSender;

namespace Moedelo.SpamV2.Client.MailSender
{
    [InjectAsSingleton]
    public class MailSenderClient : BaseApiClient, IMailSenderClient
    {
        private readonly SettingValue apiEndPoint;

        public MailSenderClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("mailServiceUrl");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<MailResponseDto> SendAsync(EmailDto request)
        {
            return PostAsync<EmailDto, MailResponseDto>("/SendEmail", request);
        }

        public Task<BaseEmailResponseDto> FillAndSendEmailAsync(EmailParamsV2Dto request)
        {
            return PostAsync<EmailParamsV2Dto, BaseEmailResponseDto>("/V2/FillAndSendEmailAsync", request);
        }

        public async Task<List<SendEmailWithLabelResponseDto>> FillAndSendEmailsAsync(List<EmailParamsV2Dto> emails)
        {
            var result = await PostAsync<List<EmailParamsV2Dto>, ListWrapper<SendEmailWithLabelResponseDto>>("/FillAndSendEmails", emails).ConfigureAwait(false);
            return result.Items;
        }
    }
}
