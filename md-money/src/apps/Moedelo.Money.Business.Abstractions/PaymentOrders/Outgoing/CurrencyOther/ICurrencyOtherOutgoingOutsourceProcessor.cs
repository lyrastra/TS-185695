using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther;

public interface ICurrencyOtherOutgoingOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(CurrencyOtherOutgoingSaveRequest request);
}