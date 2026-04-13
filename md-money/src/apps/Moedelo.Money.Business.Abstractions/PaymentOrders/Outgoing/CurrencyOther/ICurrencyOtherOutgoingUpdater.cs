using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther
{
    public interface ICurrencyOtherOutgoingUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(CurrencyOtherOutgoingSaveRequest request);
    }
}