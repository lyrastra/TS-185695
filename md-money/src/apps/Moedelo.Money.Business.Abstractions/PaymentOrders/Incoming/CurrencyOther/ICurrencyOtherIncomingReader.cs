using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther
{
    public interface ICurrencyOtherIncomingReader
    {
        Task<CurrencyOtherIncomingResponse> GetByBaseIdAsync(long baseId);
    }
}
