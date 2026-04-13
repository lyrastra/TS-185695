using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public interface IRefundFromAccountablePersonValidator
    {
        Task ValidateAsync(RefundFromAccountablePersonSaveRequest request);
    }
}
