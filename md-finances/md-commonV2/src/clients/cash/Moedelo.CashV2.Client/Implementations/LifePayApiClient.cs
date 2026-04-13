using System.Threading.Tasks;
using Moedelo.CashV2.Client.Contracts;
using Moedelo.CashV2.Dto.LifePay;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CashV2.Client.Implementations
{
    [InjectAsSingleton]
    public class LifePayApiClient : BaseApiClient, ILifePayApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public LifePayApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository)
            : base(
                  httpRequestExecutor,
                  uriCreator, 
                  responseParser, auditTracer, auditScopeManager
                  )
        {
            apiEndPoint = settingRepository.Get("CashPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<bool> RegisterLifePayUserAsync(int firmId, int userId, LifePayIntegrationClientData registerLifePayUserClientData)
        {
            return PostAsync<LifePayIntegrationClientData, bool>($"/LifePay/RegisterLifePayUser?firmId={firmId}&userId={userId}", registerLifePayUserClientData);
        }
    }
}
