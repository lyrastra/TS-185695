using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.KontragentsV2.Dto.CommissionAgents;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    [InjectAsSingleton(typeof(ICommissionAgentsApiClient))]
    public class CommissionAgentsApiClient : BaseApiClient, ICommissionAgentsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public CommissionAgentsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, 
                auditTracer, 
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<CreateCommissionByInnResultDto> CreateByInnAsync(int firmId, int userId, string inn)
        {
            if (string.IsNullOrEmpty(inn))
            {
                throw new ArgumentException(nameof(inn));
            }
            
            return PostAsync<CreateCommissionByInnResultDto>($"/CommissionAgents/CreateByInn?firmId={firmId}&userId={userId}&inn={inn}");
        }
    }
}