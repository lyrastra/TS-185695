using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.SettlementAccountEx;

namespace Moedelo.RequisitesV2.Client.SettlementAccountEx
{
    [InjectAsSingleton]
    public class SettlementAccountExApiClient : BaseApiClient, ISettlementAccountExApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public SettlementAccountExApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<SettlementAccountExDto> GetBySettlementAccountIdAsync(int firmId, int userId, int settlementAccountId)
        {
            var uri = $"/SettlementAccountEx/BySettlementAccountId/{settlementAccountId}?firmId={firmId}&userId={userId}";
            return GetAsync<SettlementAccountExDto>(uri);
        }
        
        public Task SaveOrUpdateAsync(int firmId, int userId, SettlementAccountExSaveDto saveDto)
        {
            var uri = $"/SettlementAccountEx?firmId={firmId}&userId={userId}";
            return PostAsync<SettlementAccountExSaveDto, Task>(uri, saveDto);
        }

        public Task DeleteBySettlementAccountIdAsync(int firmId, int userId, int settlementAccountId)
        {
            var uri = $"/SettlementAccountEx/BySettlementAccountId/{settlementAccountId}?firmId={firmId}&userId={userId}";
            return DeleteAsync(uri);
        }
    }
}