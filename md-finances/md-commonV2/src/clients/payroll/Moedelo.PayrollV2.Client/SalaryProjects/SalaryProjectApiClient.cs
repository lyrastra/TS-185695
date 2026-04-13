using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.PayrollV2.Client.SalaryProjects
{
    [InjectAsSingleton]
    public class SalaryProjectApiClient : BaseApiClient, ISalaryProjectApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public SalaryProjectApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/SalaryProjects";
        }

        public Task<List<int>> GetSalaryProjectSettlementAccountIds(int firmId, int userId)
        {
            return GetAsync<List<int>>("/GetSalaryProjectSettlementAccountIds", new { firmId, userId });
        }
        
        public Task UnbindRegistryPaymentAsync(int firmId, int userId, long documentBaseId)
        {
            return PostAsync(
                $"/UnbindRegistryPaymentAsync?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}");
        }
    }
}
