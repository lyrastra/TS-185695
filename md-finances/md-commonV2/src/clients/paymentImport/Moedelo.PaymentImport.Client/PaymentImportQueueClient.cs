using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PaymentImport.Dto;
using System.Threading.Tasks;

namespace Moedelo.PaymentImport.Client
{
    [InjectAsSingleton]
    public class PaymentImportQueueClient : BaseApiClient, IPaymentImportQueueClient
    {
        private const string prefix = "/private/api/v1";

        private readonly SettingValue apiEndpoint;

        public PaymentImportQueueClient(
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
            apiEndpoint = settingRepository.Get("PaymentImportHandlerApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task AppendReconcilationEventAsync(ReconciliationEventAppendRequestDto dto)
        {
            return PostAsync($"{prefix}/PaymentImportQueue/Append/Reconciliation", dto);
        }
    }
}