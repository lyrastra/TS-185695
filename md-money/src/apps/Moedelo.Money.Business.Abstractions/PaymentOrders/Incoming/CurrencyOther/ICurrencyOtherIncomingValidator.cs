using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther
{
    public interface ICurrencyOtherIncomingValidator
    {
        Task ValidateAsync(CurrencyOtherIncomingSaveRequest saveRequest);
    }
}