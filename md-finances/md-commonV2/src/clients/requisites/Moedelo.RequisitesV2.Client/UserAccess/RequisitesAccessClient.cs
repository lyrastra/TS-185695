using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.UserAccess;

namespace Moedelo.RequisitesV2.Client.UserAccess
{
    [InjectAsSingleton]
    public class RequisitesAccessClient : BaseApiClient, IRequisitesAccessClient
    {
        private readonly SettingValue apiEndPoint;
        
        public RequisitesAccessClient(
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
        
        public Task<MoneyAccessDto> GetMoneyAccessAsync(int firmId, int userId)
        {
            return GetAsync<MoneyAccessDto>("/Access/Money", new { firmId, userId});
        }
    }
}