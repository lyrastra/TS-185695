using Moedelo.Chat.Client.Base;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.Chat.Requests.Client
{
    [InjectAsSingleton]
    public class ChatPlatformRequestClient: ChatPlatformBasePrivateApiClient, IChatPlatformRequestClient
    {
        public ChatPlatformRequestClient
            (
                IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager
            )
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager, settingRepository)
        { }

        public Task BindClientDataToRequestByLoginAsync(Guid requestId, string login)
        {
            return PostAsync($"/Rest/Request/Bind/ByLogin?requestId={requestId}&login={login}");
        }

        public Task BindClientDataToRequestByUserIdAsync(Guid requestId, int userId)
        {
            return PostAsync($"/Rest/Request/Bind/ById?requestId={requestId}&id={userId}");
        }
    }
}
