using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierReader : IPaymentOrderReader<PaymentToSupplierResponse>
    {
        Task<IReadOnlyCollection<PaymentToSupplierResponse>> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
