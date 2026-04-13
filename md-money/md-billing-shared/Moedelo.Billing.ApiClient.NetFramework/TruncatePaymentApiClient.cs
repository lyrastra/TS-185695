using Moedelo.Billing.Abstractions.Dto;
using Moedelo.Billing.Abstractions.TruncatePayment;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.Billing.ApiClient.NetFramework
{
    [InjectAsSingleton(typeof(ITruncatePaymentApiClient))]
    public class TruncatePaymentApiClient : BaseApiClient, ITruncatePaymentApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public TruncatePaymentApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        public Task TruncatePaymentAsync(TruncatePaymentRequestDto dto)
        {
            return PostAsync("/TruncatePayment/Truncate", dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
