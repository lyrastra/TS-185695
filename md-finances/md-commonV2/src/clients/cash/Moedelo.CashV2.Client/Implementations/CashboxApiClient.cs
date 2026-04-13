using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CashV2.Client.Contracts;
using Moedelo.CashV2.Dto.Cashbox;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CashV2.Client.Implementations
{
    [InjectAsSingleton]
    public class CashboxApiClient : BaseApiClient, ICashboxApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public CashboxApiClient(
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

        public Task<List<CashboxDto>> GetList(int firmId)
        {
            return GetAsync<List<CashboxDto>>($"/api/v1/cashbox?firmId={firmId}");
        }
    }
}
