using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.RptV2.Client.RequisitesMaster
{
    [InjectAsSingleton(typeof(IRequisitesMasterClient))]
    public class RequisitesMasterClient : BaseApiClient, IRequisitesMasterClient
    {
        private readonly SettingValue apiEndpoint;

        public RequisitesMasterClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.GetRequired("RptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<RequisitesMasterStatus> GetStatusAsync(int firmId, int userId, CancellationToken ct)
        {
            return GetAsync<RequisitesMasterStatus>("/RequisitesMaster/GetStatus", new {firmId, userId}, cancellationToken: ct);
        }

        public Task SetStatusAsync(int firmId, int userId, RequisitesMasterStatus status)
        {
            return PostAsync($"/RequisitesMaster/SetStatus?firmId={firmId}&userId={userId}&status={status}");
        }
    }
}
