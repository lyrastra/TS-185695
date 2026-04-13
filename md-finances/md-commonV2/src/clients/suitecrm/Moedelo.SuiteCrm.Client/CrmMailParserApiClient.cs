using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class CrmMailParserApiClient : BaseApiClient, ICrmMailParserApiClient
    {
        private readonly HttpQuerySetting setting = new HttpQuerySetting {Timeout = new TimeSpan(0, 0, 10, 0)};
        private readonly SettingValue apiEndPoint;

        public CrmMailParserApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SuiteCrmApiUrl");
        }

        public Task<bool> ParseMailAsync(string email)
        {
            return GetAsync<bool>("/Mail/Parse", new {email});
        }

        public Task<bool> ParseMarketingMailAsync(string email)
        {
            return GetAsync<bool>("/Mail/ParseThankYou", new {email});
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}

