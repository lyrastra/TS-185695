using System.Threading.Tasks;
using System.Web.Hosting;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpamV2.Dto.Messengers;

namespace Moedelo.SpamV2.Client.Messengers.Slack
{
    [InjectAsSingleton]
    public class SlackSenderClient : BaseApiClient, ISlackSenderClient
    {
        private readonly SettingValue apiEndPoint;

        public SlackSenderClient(
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
            return apiEndPoint.Value + "/Slack";
        }

        public void Send(SlackSendOptionsDto dto)
        {
            HostingEnvironment.QueueBackgroundWorkItem(async ct =>
                await PostAsync("/Send", dto).ConfigureAwait(false));
        }

        public Task SendAsync(SlackSendOptionsDto dto)
        {
            return PostAsync("/Send", dto);
        }
    }
}
