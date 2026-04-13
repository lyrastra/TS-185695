using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther
{
    public interface ICurrencyOtherOutgoingCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(CurrencyOtherOutgoingSaveRequest request);
    }
}