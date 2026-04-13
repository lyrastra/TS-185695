using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.PaymentBills;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.PaymentBill
{
    [InjectAsSingleton]
    public class PaymentBillApiClient : BaseApiClient, IPaymentBillApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PaymentBillApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        protected override string GetApiEndpoint() => apiEndPoint.Value;

        public Task<List<PaymentsAndBillsCreationResultDto>> CreatePaymentsAndBillsAsync(
            PaymentsAndBillsCreationRequestDto requestDto)
        {
            return PostAsync<PaymentsAndBillsCreationRequestDto, List<PaymentsAndBillsCreationResultDto>>(
                "/PaymentBill/Create", requestDto);
        }
    }
}