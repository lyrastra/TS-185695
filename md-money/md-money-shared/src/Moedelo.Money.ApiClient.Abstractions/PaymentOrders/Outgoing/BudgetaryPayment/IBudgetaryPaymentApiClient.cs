using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentApiClient
    {
        Task<BudgetaryPaymentDto> GetByIdAsync(long documentBaseId);

        Task<PaymentOrderSaveResponseDto> CreateAsync(BudgetaryPaymentSaveDto dto);
    }
}