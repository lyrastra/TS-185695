using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other
{
    public interface IOtherOutgoingCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(OtherOutgoingSaveRequest request);
    }
}