using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentClient : IDI
    {
        Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, BudgetaryPaymentSaveDto dto);
        
        Task<BudgetaryPaymentGetDto> GetAsync(int firmId, int userId, long documentBaseId);

        // Костыль для зарплатного мастера
        Task SetPayerKppAsync(int firmId, int userId, long documentBaseId, string kpp);
    }
}
