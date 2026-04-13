using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.PaymentLink;
using Moedelo.Billing.Abstractions.PaymentLink.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework
{
    [InjectAsSingleton(typeof(IBillingPaymentLinkApiClient))]
    internal class BillingPaymentLinkApiClient : BaseApiClient, IBillingPaymentLinkApiClient
    {
        private readonly SettingValue apiEndPoint;

        public BillingPaymentLinkApiClient(
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
            apiEndPoint = settingRepository.Get("BillingBillsApiEndpoint");
        }

        public Task<PaymentLinkDto> GetPaymentLinkInfoByGuidAsync(string linkId)
        {
            const string uri = "/v1/paymentLink";

            return GetAsync<PaymentLinkDto>(uri, new { linkId });
        }

        public Task<PaymentLinkDto> GetPaymentLinkInfoByPaymentHistoryIdAsync(int paymentHistoryId)
        {
            const string uri = "/v1/paymentLink/getById";

            return GetAsync<PaymentLinkDto>(uri, new { paymentHistoryId });
        }

        public Task<List<PaymentLinkDto>> GetPaymentLinkInfoByPaymentHistoryIdsAsync(
            IReadOnlyCollection<int> paymentHistoryIds)
        {
            const string uri = "/v1/paymentLink/getByIds";

            return PostAsync<IReadOnlyCollection<int>, List<PaymentLinkDto>>(uri, paymentHistoryIds);
        }

        public Task CreateNewGuidEntryAsync(PaymentLinkRequestDto request)
        {
            const string uri = "/v1/paymentLink/create";

            return PostAsync(uri, request);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}
