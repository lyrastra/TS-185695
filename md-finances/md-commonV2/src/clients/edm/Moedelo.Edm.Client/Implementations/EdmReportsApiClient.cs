using System;
using System.Threading.Tasks;
using Moedelo.Edm.Client.Contracts;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Edm.Client.Implementations
{
    [InjectAsSingleton]
    public class EdmReportsApiClient : BaseApiClient, IEdmReportsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdmReportsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdmPrivateApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Rest/EdmReports";
        }
        
        public Task<byte[]> GetRoamingInvitationsReportAsync(string date) =>
            GetAsync<byte[]>($"/GetRoamingInvitationsReport?date={date}");
    }
}