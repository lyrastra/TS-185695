using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.Billing;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.PaymentHistory
{
    [InjectAsSingleton]
    public class PaymentHistoryExApiClient : BaseApiClient, IPaymentHistoryExApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PaymentHistoryExApiClient(
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
        
        public Task<PaymentHistoryExDto> GetAsync(int paymentId)
        {
            return GetAsync<PaymentHistoryExDto>($"/PaymentHistoryEx/{paymentId}");
        }

        public Task<List<PaymentHistoryExDto>> GetAsync(IReadOnlyCollection<int> paymentIds)
        {
            if (!paymentIds.Any())
            {
                return Task.FromResult(new List<PaymentHistoryExDto>());
            }

            return PostAsync<IEnumerable<int>, List<PaymentHistoryExDto>>("/PaymentHistoryEx/Get", paymentIds);
        }

        public Task<List<PaymentHistoryExDto>> GetAsync(PaymentHistoryExRequestDto criteria)
        {
            return PostAsync<PaymentHistoryExRequestDto, List<PaymentHistoryExDto>>("/PaymentHistoryEx/GetByCriteria", criteria);
        }
    }
}