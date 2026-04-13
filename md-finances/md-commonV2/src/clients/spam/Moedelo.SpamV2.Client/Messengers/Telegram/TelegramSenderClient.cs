using System.Threading.Tasks;
using System.Web.Hosting;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpamV2.Dto.Messengers;

namespace Moedelo.SpamV2.Client.Messengers.Telegram
{
    [InjectAsSingleton]
    public class TelegramSenderClient : BaseApiClient, ITelegramSenderClient
    {
        private readonly SettingValue apiEndPoint;

        public TelegramSenderClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                  httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("imServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Telegram";
        }

        public void Send(TelegramSendOptionsDto dto)
        {
            HostingEnvironment.QueueBackgroundWorkItem(async ct =>
                await PostAsync("/Send", dto).ConfigureAwait(false));
        }

        public Task SendAsync(TelegramSendOptionsDto dto)
        {
            return PostAsync("/Send", dto);
        }
    }
}
