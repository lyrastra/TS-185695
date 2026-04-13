using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public interface IRefundFromAccountablePersonCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(RefundFromAccountablePersonSaveRequest request);
    }
}