using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;

public interface IOutgoingCurrencyPurchaseOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(OutgoingCurrencyPurchaseSaveRequest request);
}