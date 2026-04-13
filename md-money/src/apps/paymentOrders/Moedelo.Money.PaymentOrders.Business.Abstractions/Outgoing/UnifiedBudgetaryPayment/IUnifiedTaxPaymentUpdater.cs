using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedTaxPaymentUpdater
    {
        Task SetTaxPostingTypeAsync(long documentBaseId, ProvidePostingType taxPostingType);
    }
}