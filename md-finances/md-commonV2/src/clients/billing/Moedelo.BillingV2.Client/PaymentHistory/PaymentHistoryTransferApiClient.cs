using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.PaymentHistory;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.PaymentHistory
{
    [InjectAsSingleton]
    public class PaymentHistoryTransferApiClient : BaseApiClient, IPaymentHistoryTransferApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PaymentHistoryTransferApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task MovePaymentsAsync(int partnerUserId, int fromFirmId, int toFirmId)
        {
            var request = new PaymentHistoryTransferRequestDto
            {
                PartnerUserId = partnerUserId,
                FromFirmId = fromFirmId,
                ToFirmId = toFirmId
            };

            return PostAsync("/PaymentHistory/Transfer", request);
        }
    }
}