using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.CashOrders;
using Moedelo.Money.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentClient : IDI
    {
        Task<UnifiedBudgetaryPaymentGetDto> GetAsync(int firmId, int userId, long documentBaseId);
        Task<CashOrderSaveResponseDto> CreateAsync(int firmId, int userId, UnifiedBudgetaryPaymentSaveDto dto);
        Task<CashOrderSaveResponseDto> UpdateAsync(int firmId, int userId, UnifiedBudgetaryPaymentSaveDto dto);

    }
}
