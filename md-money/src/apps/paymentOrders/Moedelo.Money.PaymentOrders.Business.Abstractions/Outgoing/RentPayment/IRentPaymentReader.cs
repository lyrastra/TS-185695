using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment
{
    public interface IRentPaymentReader
    {
        Task<PaymentOrderResponse> GetByBaseIdAsync(long documentBaseId);
    }
}
