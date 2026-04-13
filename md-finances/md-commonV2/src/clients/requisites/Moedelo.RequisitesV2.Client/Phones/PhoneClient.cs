using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.Phones;

namespace Moedelo.RequisitesV2.Client.Phones
{
    [InjectAsSingleton]
    public class PhoneClient : BaseApiClient,  IPhoneClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PhoneClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager
            ) 
            :base (httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<PhoneDto>> GetAllAsync(int firmId)
        {
            return GetAsync<List<PhoneDto>>("/Phones", new { firmId });
        }
    }
}