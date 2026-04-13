using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther
{
    public interface ICurrencyOtherIncomingCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(CurrencyOtherIncomingSaveRequest request);
    }
}