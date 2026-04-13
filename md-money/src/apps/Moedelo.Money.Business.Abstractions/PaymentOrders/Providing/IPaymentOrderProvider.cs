using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Providing
{
    public interface IPaymentOrderProvider
    {
        Task ProvideAsync(long documentBaseId);

        Task ProvideAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
