using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.RequisitesV2.Client.Pfr
{
    [InjectAsSingleton]
    public class PfrClient : BaseApiClient, IPfrClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PfrClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository) : base(
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

        public Task<bool> IsPfrRequisitesChanged(int firmId, int userId, bool havePfrSignature, bool edsComplete)
        {
            return GetAsync<bool>("/Pfr/IsPfrRequisitesChanged", new { firmId, userId, havePfrSignature, edsComplete });
        }
    }
}