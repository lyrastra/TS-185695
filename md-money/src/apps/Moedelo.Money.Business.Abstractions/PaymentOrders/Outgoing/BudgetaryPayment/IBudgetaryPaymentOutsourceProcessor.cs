using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;

public interface IBudgetaryPaymentOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(BudgetaryPaymentSaveRequest request);
}