using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.Emails.Client
{
    [InjectAsSingleton]
    public class BpmEmailsClient : BaseApiClient, IBpmEmailsClient
    {
        private readonly SettingValue apiEndpoint;

        public BpmEmailsClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }

        public Task<bool> MailParseAsync(string email, string backupEmail = null, int count = 1)
            => GetAsync<bool>($"/Rest/Mail/Parse", new {email, backup_email = backupEmail, count});
        
        public Task<string> GetMailRuTokenAsync(string code, string state)
            => GetAsync<string>($"/MailRu/oauth/token", new {code, state});

        public Task ClientMailParseAsync()
            => GetAsync("/Rest/Mailboxes/Parse");
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/emails";
        }
    }
}