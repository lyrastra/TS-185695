using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonValidator
    {
        Task ValidateCreateRequestAsync(PaymentToAccountablePersonSaveRequest request);
        Task ValidateUpdateRequestAsync(PaymentToAccountablePersonSaveRequest request);
    }
}