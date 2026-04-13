using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Providing
{
    public interface IPaymentOrdersAsyncBatchProvider
    {
        Task ProvideAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
