using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.PfrEdmStatement
{
    [InjectAsSingleton]
    public class PfrEdmStatementClient : BaseApiClient, IPfrEdmStatementClient
    {
        private readonly SettingValue apiEndpoint;

        public PfrEdmStatementClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<PfrEdmStatementStatus> GetStatusAsync(int firmId, int userId) =>
            GetAsync<PfrEdmStatementStatus>($"/PfrEdmStatement/GetStatus?firmId={firmId}&userId={userId}");

        public Task ResetStatusAsync(int firmId) =>
            PostAsync($"/PfrEdmStatement/ResetStatus?firmId={firmId}");
    }
}