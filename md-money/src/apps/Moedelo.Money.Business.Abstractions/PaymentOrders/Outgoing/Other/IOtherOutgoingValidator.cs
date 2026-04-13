using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other
{
    public interface IOtherOutgoingValidator
    {
        Task ValidateAsync(OtherOutgoingSaveRequest request);
    }
}
