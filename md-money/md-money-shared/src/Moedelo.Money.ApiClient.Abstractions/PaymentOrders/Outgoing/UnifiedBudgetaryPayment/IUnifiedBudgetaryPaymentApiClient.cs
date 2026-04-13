using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;

public interface IUnifiedBudgetaryPaymentApiClient
{
    Task<string> GetDescriptionAsync(IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> subPaymentsDto);

    Task<PaymentOrderSaveResponseDto> CreateAsync(UnifiedBudgetaryPaymentSaveDto dto);
    Task<UnifiedBudgetaryPaymentDto> GetAsync(long documentBaseId);
    Task DeleteAsync(long documentBaseId);
}