using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract
{
    public interface IAgencyContractUpdater : IPaymentOrderUpdater<AgencyContractSaveRequest, PaymentOrderSaveResponse>
    {
    }
}