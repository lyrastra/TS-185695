using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other
{
    public interface IOtherIncomingCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(OtherIncomingSaveRequest request);
    }
}