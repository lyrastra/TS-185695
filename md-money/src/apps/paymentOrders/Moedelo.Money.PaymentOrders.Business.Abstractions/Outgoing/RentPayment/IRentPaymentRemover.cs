using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment
{
    public interface IRentPaymentRemover
    {
        Task DeleteAsync(long documentBaseId);
    }
}
