using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther
{
    public interface ICurrencyOtherOutgoingValidator
    {
        Task ValidateAsync(CurrencyOtherOutgoingSaveRequest request);
    }
}