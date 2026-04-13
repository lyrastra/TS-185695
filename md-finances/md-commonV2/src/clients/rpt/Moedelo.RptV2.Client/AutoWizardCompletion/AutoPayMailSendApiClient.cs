using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto.AutoWizardCompletion;

namespace Moedelo.RptV2.Client.AutoWizardCompletion
{
    [InjectAsSingleton]
    public class AutoPayMailSendApiClient : BaseApiClient, IAutoPayMailSendApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public AutoPayMailSendApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        public async Task<bool> SendMailAsync(SendMailRequest mailRequest)
        {
            return await PostAsync<SendMailRequest, bool>
                ($"/AutoPayMailSendApi/SendMail?userId={mailRequest.UserId}&firmId={mailRequest.FirmId}", mailRequest)
                .ConfigureAwait(false);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}