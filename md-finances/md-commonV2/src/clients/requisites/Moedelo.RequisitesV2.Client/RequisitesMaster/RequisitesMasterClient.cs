using System;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.RequisitesV2.Client.RequisitesMaster
{
    [InjectAsSingleton]
    public class RequisitesMasterClient : BaseApiClient, IRequisitesMasterClient
    {
        private readonly SettingValue apiEndpoint;

        public RequisitesMasterClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<bool> GetLockingStatus(int firmId)
        {
            return GetAsync<bool>($"/RequisitesMaster/GetLockingStatus?firmId={firmId}");
        }

        [Obsolete("Use Moedelo.RptV2.Client.RequisitesMaster.RequisitesMasterClient")]
        public Task<RequisitesMasterStatus> GetStatus(int firmId, int userId)
        {
            return GetAsync<RequisitesMasterStatus>($"/RequisitesMaster/Status?firmId={firmId}&userId={userId}");
        }
    }
}
