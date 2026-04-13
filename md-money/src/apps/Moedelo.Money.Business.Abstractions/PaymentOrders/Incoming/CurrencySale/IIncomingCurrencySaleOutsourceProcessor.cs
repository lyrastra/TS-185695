using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;

public interface IIncomingCurrencySaleOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(IncomingCurrencySaleSaveRequest request);
}