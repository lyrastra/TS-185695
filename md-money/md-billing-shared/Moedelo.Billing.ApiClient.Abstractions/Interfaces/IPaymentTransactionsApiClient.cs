using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentTransactions;

namespace Moedelo.Billing.Abstractions.Interfaces;

public interface IPaymentTransactionsApiClient
{
    Task<IReadOnlyCollection<PaymentTransactionDto>> GetTransactionsByCriteriaAsync(GetTransactionsCriteriaDto dto);

    Task<IReadOnlyCollection<PaymentTransactionDto>> GetNotRecognizedAsync(NotRecognizedTransactionsRequest request);

    Task<IReadOnlyCollection<int>> GetIndividualTransactionsIdsAsync(IReadOnlyCollection<int> paymentImportDetailIds);
    
    Task LinkTransactionsToPaymentAsync(IReadOnlyCollection<PaymentTransactionLinkToPaymentDto> transactionsForLink);

    Task UnLinkByTransactionIdsAsync(IReadOnlyCollection<PaymentTransactionUnLinkFromPaymentDto> transactionsForUnLink);

    Task UnLinkByPaymentHistoryIdAsync(int paymentHistoryId);
}