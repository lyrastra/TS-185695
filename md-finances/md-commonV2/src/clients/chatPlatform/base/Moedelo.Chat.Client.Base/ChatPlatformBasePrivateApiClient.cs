using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Chat.Client.Base
{
    public abstract class ChatPlatformBasePrivateApiClient : BaseApiClient
    {
        private readonly SettingValue privateApiEndpoint;

        protected ChatPlatformBasePrivateApiClient
        (
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository
        )
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            privateApiEndpoint = settingRepository.Get("ChatPlatformPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return privateApiEndpoint.Value;
        }
    }
}