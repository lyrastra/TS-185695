using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Consultations.Client.ConsultationThread
{
    [InjectAsSingleton]
    public class ConsultationThreadApiClient : BaseApiClient, IConsultationThreadApiClient
    {
        private readonly SettingValue apiEndpoint;
        private const string ControllerName = "/Thread/";

        public ConsultationThreadApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ConsultationsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}{ControllerName}";
        }
        
        public Task<bool> HasAnyThreadAsync(int firmId)
        {
            return GetAsync<bool>("HasAnyThread", new { firmId });
        }
    }
}