using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentTransactions;
using Moedelo.Billing.Abstractions.Interfaces;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework
{
    [InjectAsSingleton(typeof(IPaymentTransactionsApiClient))]
    public class PaymentTransactionsApiClient : BaseCoreApiClient, IPaymentTransactionsApiClient
    {
        private readonly SettingValue endpoint;

        public PaymentTransactionsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            settingRepository,
            auditTracer,
            auditScopeManager)
        {
            endpoint = settingRepository.Get("BillingPaymentTransactionsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public Task<IReadOnlyCollection<PaymentTransactionDto>> GetTransactionsByCriteriaAsync(GetTransactionsCriteriaDto dto)
        {
            return PostAsync<GetTransactionsCriteriaDto, IReadOnlyCollection<PaymentTransactionDto>>(
                "/api/v1/PaymentTransactions/GetByCriteria", dto);
        }

        public Task<IReadOnlyCollection<PaymentTransactionDto>> GetNotRecognizedAsync(NotRecognizedTransactionsRequest request)
        {
            return PostAsync<NotRecognizedTransactionsRequest, IReadOnlyCollection<PaymentTransactionDto>>(
                "/api/v1/PaymentTransactions/GetNotRecognized", request);
        }

        public Task<IReadOnlyCollection<int>> GetIndividualTransactionsIdsAsync(IReadOnlyCollection<int> paymentImportDetailIds)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<int>>(
                "/api/v1/PaymentTransactions/GetIndividualTransactionsIds", paymentImportDetailIds);
        }

        public Task LinkTransactionsToPaymentAsync(IReadOnlyCollection<PaymentTransactionLinkToPaymentDto> transactionsForLink)
        {
            return PostAsync<IReadOnlyCollection<PaymentTransactionLinkToPaymentDto>>(
                "/api/v1/PaymentTransactions/LinkToPayment", transactionsForLink);
        }

        public Task UnLinkByTransactionIdsAsync(IReadOnlyCollection<PaymentTransactionUnLinkFromPaymentDto> transactionsForUnLink)
        {
            return PostAsync<IReadOnlyCollection<PaymentTransactionUnLinkFromPaymentDto>>(
                "/api/v1/PaymentTransactions/UnLinkByTransactionIds", transactionsForUnLink);
        }

        public Task UnLinkByPaymentHistoryIdAsync(int paymentHistoryId)
        {
            return PostAsync($"/api/v1/PaymentTransactions/UnLinkByPaymentHistoryId/{paymentHistoryId}");
        }
    }
}
