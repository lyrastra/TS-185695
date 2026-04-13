using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment
{
    public interface IRentPaymentCreator
    {
        Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request);
    }
}
