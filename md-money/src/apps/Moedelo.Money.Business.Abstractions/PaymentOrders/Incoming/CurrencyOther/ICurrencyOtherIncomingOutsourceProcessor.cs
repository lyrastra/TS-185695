using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther;

public interface ICurrencyOtherIncomingOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(CurrencyOtherIncomingSaveRequest request);
}