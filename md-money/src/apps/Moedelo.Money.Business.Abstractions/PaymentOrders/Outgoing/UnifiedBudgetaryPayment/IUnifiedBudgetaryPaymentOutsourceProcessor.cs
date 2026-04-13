using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;

public interface IUnifiedBudgetaryPaymentOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(UnifiedBudgetaryPaymentSaveRequest request);
}