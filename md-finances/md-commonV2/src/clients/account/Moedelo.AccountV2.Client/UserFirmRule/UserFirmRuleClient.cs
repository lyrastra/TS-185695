using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.UserFirmRule
{
    [InjectAsSingleton]
    public class UserFirmRuleClient : BaseApiClient, IUserFirmRuleClient
    {
        private readonly SettingValue apiEndPoint;
        
        public UserFirmRuleClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator,
            IResponseParser responseParser, 
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base( httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("UserFirmRuleEndpoint");
        }

        public Task<List<AccessRule>> GetRulesAsync(int firmId, int userId)
        {
            return GetAsync<List<AccessRule>>($"/GetRules?firmId={firmId}&userId={userId}");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/V2";
        }
    }
}