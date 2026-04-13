using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonIgnoreNumberSaver
    {
        Task ApplyIgnoreNumberAsync(PaymentToAccountablePersonApplyIgnoreNumberRequest applyRequest);
    }
}