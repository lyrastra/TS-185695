using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.TariffsAndRoles;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.TariffsAndRoles
{
    [InjectAsSingleton]
    public class TariffsAndRolesApiClient : BaseApiClient, ITariffsAndRolesApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public TariffsAndRolesApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<TariffsAndRolesDto> GetAllAsync()
        {
            return GetAsync<TariffsAndRolesDto>("Rest/TariffsAndRoles/All");
        }

        public Task InvalidateCacheAsync(int firmId, int userId)
        {
            return GetAsync($"Rest/TariffsAndRoles/Invalidate?FirmId={firmId}&UserId={userId}");
        }
    }
}