using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;

public interface IIncomingCurrencyPurchaseOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(IncomingCurrencyPurchaseSaveRequest request);
}