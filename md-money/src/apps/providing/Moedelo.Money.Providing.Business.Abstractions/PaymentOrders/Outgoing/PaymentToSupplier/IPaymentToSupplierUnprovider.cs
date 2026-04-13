using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierUnprovider
    {
        Task UnprovideAsync(long documentBaseId);
    }
}
