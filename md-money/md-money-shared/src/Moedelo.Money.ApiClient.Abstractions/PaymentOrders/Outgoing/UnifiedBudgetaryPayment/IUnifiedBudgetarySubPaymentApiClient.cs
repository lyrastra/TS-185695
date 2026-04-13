using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetarySubPaymentApiClient
    {
        Task<long> GetParentIdAsync(long documentBaseId);

        Task<IReadOnlyCollection<UnifiedBudgetarySubPaymentResponsePrivateDto>> GetByParentIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}