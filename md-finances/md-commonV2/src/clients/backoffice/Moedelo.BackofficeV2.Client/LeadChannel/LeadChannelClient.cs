using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.LeadChannel
{
    [InjectAsSingleton]
    public class LeadChannelClient : BaseApiClient, ILeadChannelClient
    {
        private const string ControllerUri = "/Rest/LeadChannel/V2/";
        private readonly SettingValue apiEndPoint;

        public LeadChannelClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BackOfficePrivateApiEndpoint");
        }

        public Task RecalcChannelForFirmsAsync(IEnumerable<int> firmIds)
        {
            return PostAsync<IEnumerable<int>>("RecalcChannelForFirms", firmIds);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerUri;
        }
    }
}