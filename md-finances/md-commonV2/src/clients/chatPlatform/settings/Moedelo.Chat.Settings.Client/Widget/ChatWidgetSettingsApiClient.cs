using System.Threading;
using System.Threading.Tasks;
using Moedelo.Chat.Client.Base;
using Moedelo.Chat.Settings.Client.Abstractions.Widget;
using Moedelo.Chat.Settings.Client.Abstractions.Widget.Dto;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.Chat.Settings.Client.Widget
{
    [InjectAsSingleton(typeof(IChatWidgetSettingsApiClient))]
    public class ChatWidgetSettingsApiClient : ChatPlatformBasePrivateApiClient, IChatWidgetSettingsApiClient
    {
        public ChatWidgetSettingsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                  httpRequestExecutor,
                  uriCreator,
                  responseParser,
                  auditTracer,
                  auditScopeManager,
                  settingRepository)
        {
        }

        public Task<ChatWidgetSettingsDto> GetWidgetSettingsAsync(CancellationToken cancellationToken)
        {
            const string uri = "Settings/Widget";

            return GetAsync<ChatWidgetSettingsDto>(uri, cancellationToken: cancellationToken);
        }
    }
}
