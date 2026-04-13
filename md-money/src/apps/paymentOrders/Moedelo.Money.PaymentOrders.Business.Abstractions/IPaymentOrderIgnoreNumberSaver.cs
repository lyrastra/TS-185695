using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions
{
    public interface IPaymentOrderIgnoreNumberSaver
    {
        Task ApplyIgnoreNumberAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}