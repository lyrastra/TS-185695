using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;

public interface IOutgoingCurrencySaleOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(OutgoingCurrencySaleSaveRequest request);
}