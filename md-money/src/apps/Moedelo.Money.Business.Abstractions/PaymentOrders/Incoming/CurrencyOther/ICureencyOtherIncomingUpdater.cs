using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther
{
    public interface ICurrencyOtherIncomingUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(CurrencyOtherIncomingSaveRequest request);
    }
}