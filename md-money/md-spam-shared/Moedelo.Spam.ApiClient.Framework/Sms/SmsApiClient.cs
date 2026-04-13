using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Spam.ApiClient.Abastractions.Dto.Sms;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.Sms;

namespace Moedelo.Spam.ApiClient.Framework.Sms
{
    [InjectAsSingleton(typeof(ISmsApiClient))]
    internal sealed class SmsApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager) : BaseApiClient(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager), ISmsApiClient
    {
        private readonly SettingValue apiEndPoint = settingRepository.Get("SmsApiEndpoint");
        private static readonly HttpQuerySetting ExtendedTimeoutQuerySetting = new(TimeSpan.FromSeconds(120));

        public Task<SendSmsResponseDto> SendAsync(SendSmsRequestDto request, CancellationToken cancellationToken)
        {
            return PostAsync<SendSmsRequestDto, SendSmsResponseDto>(
                "/api/v1/sms/send",
                request,
                setting: ExtendedTimeoutQuerySetting,
                cancellationToken: cancellationToken);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}