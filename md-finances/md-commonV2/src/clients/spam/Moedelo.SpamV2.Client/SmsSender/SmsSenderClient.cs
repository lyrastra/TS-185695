using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpamV2.Dto.SmsSender;

namespace Moedelo.SpamV2.Client.SmsSender
{
    [InjectAsSingleton]
    public class SmsSenderClient : BaseApiClient, ISmsSenderClient
    {
        private readonly SettingValue apiEndPoint;

        public SmsSenderClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository
            )
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("smsServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<SmsSendResponseDto>> SendSmsAsync(SmsSendRequestDto dto)
        {
            return PostAsync<SmsSendRequestDto, List<SmsSendResponseDto>>("/V2/SendSms", dto);
        }
    }
}