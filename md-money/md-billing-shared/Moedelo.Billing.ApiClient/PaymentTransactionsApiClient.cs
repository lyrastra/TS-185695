using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Dto.PaymentTransactions;
using Moedelo.Billing.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients;

[InjectAsSingleton(typeof(IPaymentTransactionsApiClient))]
internal class PaymentTransactionsApiClient : BaseApiClient, IPaymentTransactionsApiClient
{
    public PaymentTransactionsApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<PaymentTransactionsApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("BillingPaymentTransactionsApiEndpoint"),
            logger)
    {
    }

    public Task<IReadOnlyCollection<PaymentTransactionDto>> GetTransactionsByCriteriaAsync(
        GetTransactionsCriteriaDto dto)
    {
        return PostAsync<GetTransactionsCriteriaDto, IReadOnlyCollection<PaymentTransactionDto>>(
            "/api/v1/PaymentTransactions/GetByCriteria",
            dto);
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

    public Task LinkTransactionsToPaymentAsync(
        IReadOnlyCollection<PaymentTransactionLinkToPaymentDto> transactionsForLink)
    {
        return PostAsync<IReadOnlyCollection<PaymentTransactionLinkToPaymentDto>>(
            "/api/v1/PaymentTransactions/LinkToPayment",
            transactionsForLink);
    }

    public Task UnLinkByTransactionIdsAsync(
        IReadOnlyCollection<PaymentTransactionUnLinkFromPaymentDto> transactionsForUnLink)
    {
        return PostAsync<IReadOnlyCollection<PaymentTransactionUnLinkFromPaymentDto>>(
            "/api/v1/PaymentTransactions/UnLinkByTransactionIds",
            transactionsForUnLink);
    }

    public Task UnLinkByPaymentHistoryIdAsync(int paymentHistoryId)
    {
        return PostAsync($"/api/v1/PaymentTransactions/UnLinkByPaymentHistoryId/{paymentHistoryId}");
    }
}