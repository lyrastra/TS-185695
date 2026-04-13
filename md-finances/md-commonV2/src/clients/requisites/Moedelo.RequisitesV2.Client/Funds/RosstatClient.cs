using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.RequisitesV2.Dto.Funds;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.RequisitesV2.Client.Funds
{
    [InjectAsSingleton]
    public class RosstatClient : BaseApiClient, IRosstatClient
    {
        private readonly SettingValue apiEndPoint;
        
        public RosstatClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<RosstatDepartmentDto> GetDepartmentAsync(int firmId, int userId)
        {
            return GetAsync<RosstatDepartmentDto>("/Rosstat/Department", new { firmId, userId });
        }

        public Task SaveDepartmentAsync(int firmId, int userId, RosstatDepartmentDto department)
        {
            return PostAsync($"/Rosstat/Department?firmId={firmId}&userId={userId}", department);
        }
    }
}